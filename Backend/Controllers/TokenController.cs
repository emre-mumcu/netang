using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Backend.AppLib.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Backend;

public class TokenController : MyControllerBase
{
    private readonly ITokenService _tokenService;

    public TokenController(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }

    [HttpGet("[action]")]
    public IActionResult Create()
    {
        var userClaims = new List<Claim>{
            new Claim(JwtRegisteredClaimNames.NameId, "12345"),
            new Claim(JwtRegisteredClaimNames.GivenName, "emre-mumcu")
        };

        string token = _tokenService.CreateToken(new ClaimsIdentity(userClaims));

        return Ok(ApiResults.Success(token));
    }

    [HttpPost("[action]")]
    public IActionResult Validate([FromForm]string Token)
    {
        string message;      

        SecurityToken? token;

        bool result = _tokenService.ValidateToken(Token, out message, out token);

        if(result) return Ok(ApiResults.Success(token));
        else return Ok(ApiResults.Error(message));
    }

    [HttpGet("[action]")]
    public IActionResult CreateEncrypted()
    {
        var userClaims = new List<Claim>{
            new Claim(JwtRegisteredClaimNames.NameId, "12345"),
            new Claim(JwtRegisteredClaimNames.GivenName, "emre-mumcu")
        };

        string token = _tokenService.CreateTokenEncrypted(new ClaimsIdentity(userClaims));

        return Ok(ApiResults.Success(token));
    }

    [HttpPost("[action]")]
    public IActionResult ValidateEncrypted([FromForm] string Token)
    {
        string message;

        SecurityToken? token;

        bool result = _tokenService.ValidateTokenEncrypted(Token, out message, out token);

        if (result) return Ok(ApiResults.Success(token));
        else return BadRequest();
    }
}
