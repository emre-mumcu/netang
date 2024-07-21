using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Backend.App_Lib.Configuration;
using Backend.AppLib.Contracts;
using Backend.AppLib.Extensions;
using Microsoft.IdentityModel.Tokens;

namespace Backend.AppLib.Services;

// dotnet add package Microsoft.IdentityModel.Tokens
// dotnet add package System.IdentityModel.Tokens.Jwt

public partial class TokenService : ITokenService
{
    /*
        var hash = SHA512.Create().ComputeHash(Encoding.UTF8.GetBytes(CM.DataConfiguration["TokenParameters:SignKey"]!));    
        private static string GetStringFromHash(byte[] hash)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < hash.Length; i++) result.Append(hash[i].ToString("X2"));
            return result.ToString();
        }
    */
    private SecurityKey GetSecurityKey(string key) => new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));

    private SigningCredentials GetSigningCredentials()
    {
        SigningCredentials signingCredentials = new SigningCredentials(
            key: GetSecurityKey(AppConfig.Instance.DataConfiguration["TokenParameters:SignKey"]!),
            algorithm: SecurityAlgorithms.HmacSha512Signature
        );

        return signingCredentials;
    }

    private EncryptingCredentials GetEncryptingCredentials()
    {
        SecurityKey securityKey = GetSecurityKey(AppConfig.Instance.DataConfiguration["TokenParameters:EncryptKey"]!);

        var (al, en) = securityKey.KeySize switch
        {
            128 => (SecurityAlgorithms.Aes128KW, SecurityAlgorithms.Aes128CbcHmacSha256),
            192 => (SecurityAlgorithms.Aes192KeyWrap, SecurityAlgorithms.Aes192CbcHmacSha384),
            256 => (SecurityAlgorithms.Aes256KW, SecurityAlgorithms.Aes256CbcHmacSha512),
            _ => throw new ArgumentException("Size of encryption key can be 128, 192, or 256 bits")
        };

        return new EncryptingCredentials(key: securityKey, alg: al, enc: en);
    }

    private SecurityTokenDescriptor GetSecurityTokenDescriptor(ClaimsIdentity subject)
    {
        return new SecurityTokenDescriptor()
        {
            IssuedAt = DateTime.UtcNow,
            NotBefore = DateTime.UtcNow,
            Subject = subject,
            Expires = DateTime.UtcNow.AddMinutes(AppConfig.Instance.DataConfiguration["TokenParameters:Timeout"]!.ToDouble()),
            Issuer = AppConfig.Instance.DataConfiguration["TokenParameters:Issuer"],
            Audience = AppConfig.Instance.DataConfiguration["TokenParameters:Audience"],
            SigningCredentials = GetSigningCredentials()
        };
    }

    private SecurityTokenDescriptor GetSecurityTokenDescriptorEncrypted(ClaimsIdentity subject)
    {
        SecurityTokenDescriptor tokenDescriptor = GetSecurityTokenDescriptor(subject);

        tokenDescriptor.EncryptingCredentials = GetEncryptingCredentials();

        return tokenDescriptor;
    }

    private TokenValidationParameters GetTokenValidationParameters()
    {
        return new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = AppConfig.Instance.DataConfiguration["TokenParameters:Issuer"],
            ValidAudience = AppConfig.Instance.DataConfiguration["TokenParameters:Audience"],
            IssuerSigningKey = GetSecurityKey(AppConfig.Instance.DataConfiguration["TokenParameters:SignKey"]!),
            ClockSkew = TimeSpan.FromMinutes(AppConfig.Instance.DataConfiguration["TokenParameters:ClockSkew"]!.ToDouble())
        };
    }

    private TokenValidationParameters GetTokenValidationParametersEncrypted()
    {
        TokenValidationParameters tokenValidationParameters = GetTokenValidationParameters();
        tokenValidationParameters.TokenDecryptionKey = GetSecurityKey(AppConfig.Instance.DataConfiguration["TokenParameters:EncryptKey"]!);
        return tokenValidationParameters;
    }

    public string CreateToken(string UserName)
    {
        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        
        var userClaims = new List<Claim>{
            new Claim(JwtRegisteredClaimNames.NameId, UserName)
        };

        ClaimsIdentity ci = new ClaimsIdentity(userClaims);

        JwtSecurityToken token = tokenHandler.CreateJwtSecurityToken(GetSecurityTokenDescriptor(ci));
        return tokenHandler.WriteToken(token);
    }

    public string CreateToken(ClaimsIdentity UserClaims)
    {
        // var jwt = new JwtSecurityToken(issuer: "", audience: "", claims: null, notBefore: null, expires: null, signingCredentials: null);
        // string tkn = new JwtSecurityTokenHandler().WriteToken(jwt);

        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        // SecurityToken token = tokenHandler.CreateToken(GetSecurityTokenDescriptor(UserClaims));
        JwtSecurityToken token = tokenHandler.CreateJwtSecurityToken(GetSecurityTokenDescriptor(UserClaims));
        return tokenHandler.WriteToken(token);
    }

    public string CreateTokenEncrypted(ClaimsIdentity UserClaims)
    {
        // var jwt = new JwtSecurityToken(issuer: "", audience: "", claims: null, notBefore: null, expires: null, signingCredentials: null);
        // string tkn = new JwtSecurityTokenHandler().WriteToken(jwt);

        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        // SecurityToken token = tokenHandler.CreateToken(GetSecurityTokenDescriptor(UserClaims));
        JwtSecurityToken token = tokenHandler.CreateJwtSecurityToken(GetSecurityTokenDescriptorEncrypted(UserClaims));
        return tokenHandler.WriteToken(token);
    }

    public bool ValidateToken(string Token, out string Message, out SecurityToken? JWT)
    {
        try
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            ClaimsPrincipal user = tokenHandler.ValidateToken(Token, GetTokenValidationParameters(), out JWT);
            Message = "Token is validated";
            return true;
        }
        catch (Exception ex)
        {
            Message = $"{ex.Message}";
            JWT = null;
            return false;
        }
    }

    public bool ValidateTokenEncrypted(string Token, out string Message, out SecurityToken? JWT)
    {
        try
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            ClaimsPrincipal user = tokenHandler.ValidateToken(Token, GetTokenValidationParametersEncrypted(), out JWT);
            Message = "Token is validated";
            return true;
        }
        catch (Exception ex)
        {
            Message = $"{ex.Message}";
            JWT = null;
            return false;
        }
    }
}