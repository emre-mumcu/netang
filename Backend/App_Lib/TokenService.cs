using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;

// dotnet add package System.IdentityModel.Tokens.Jwt

namespace Backend;

public interface ITokenService
{
    string CreateToken(UserDto user);
}

public class TokenService : ITokenService
{
    public string CreateToken(UserDto user)
    {
        SHA512 shaM = SHA512.Create();

        var hash = shaM.ComputeHash(Encoding.UTF8.GetBytes(MyConfigurationManager.Data["JWT:SecurityKey"]!));

        SymmetricSecurityKey? securityKey = new SymmetricSecurityKey(hash);

        SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);

        var userClaims = new List<Claim>{
            new Claim(JwtRegisteredClaimNames.NameId, user.UserId.ToString()),
            new Claim(JwtRegisteredClaimNames.GivenName, user.UserName)
        };

        SecurityTokenDescriptor tokenDecriptor = new SecurityTokenDescriptor
        {
            Issuer = MyConfigurationManager.Data["JWT:Issuer"],
            Audience = MyConfigurationManager.Data["JWT:Audience"],
            IssuedAt = DateTime.Now,
            NotBefore = DateTime.Now,
            Subject = new ClaimsIdentity(userClaims),
            Expires = DateTime.Now.AddDays(7),
            SigningCredentials = signingCredentials
        };

        // var tokeOptions = new JwtSecurityToken(issuer: "", audience: "", claims: new List<Claim>(), expires: DateTime.Now.AddMinutes(6), signingCredentials: signingCredentials);
        // var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);

        var tokenHandler = new JwtSecurityTokenHandler();

        SecurityToken token = tokenHandler.CreateToken(tokenDecriptor);

        return tokenHandler.WriteToken(token);
    }

    private static string GetStringFromHash(byte[] hash)
    {
        StringBuilder result = new StringBuilder();
        for (int i = 0; i < hash.Length; i++)
        {
            result.Append(hash[i].ToString("X2"));
        }
        return result.ToString();
    }
}