using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Backend.App_Lib;
using Backend.App_Lib.Common;
using Backend.AppLib.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Backend;

public class TokenController : MyControllerBase
{
    private readonly ITokenService _tokenService;
    private readonly ILogger<TokenController> _logger;

    public TokenController(ITokenService tokenService, ILogger<TokenController> logger)
    {
        _tokenService = tokenService;
        _logger = logger;
    }

    [HttpGet("[action]")]
    public IActionResult Create()
    {
        var userClaims = new List<Claim>{
            new Claim(JwtRegisteredClaimNames.NameId, "12345"),
            new Claim(JwtRegisteredClaimNames.GivenName, "emre-mumcu")
        };

        string token = _tokenService.CreateToken(new ClaimsIdentity(userClaims));

        _logger.LogInformation("Token Created");

        return Ok(ApiResponses.Success(token));
    }

    [HttpPost("[action]")]
    public IActionResult Validate([FromForm] string Token)
    {
        string message;

        SecurityToken? token;

        bool result = _tokenService.ValidateToken(Token, out message, out token);

        if (result) return Ok(ApiResponses.Success(token));
        else return Ok(ApiResponses.Fail(message));
    }

    [HttpGet("[action]")]
    public IActionResult CreateEncrypted()
    {
        var userClaims = new List<Claim>{
            new Claim(JwtRegisteredClaimNames.NameId, "12345"),
            new Claim(JwtRegisteredClaimNames.GivenName, "emre-mumcu")
        };

        string token = _tokenService.CreateTokenEncrypted(new ClaimsIdentity(userClaims));

        return Ok(ApiResponses.Success(token));
    }

    [HttpPost("[action]")]
    public IActionResult ValidateEncrypted([FromForm] string Token)
    {
        string message;

        SecurityToken? token;

        bool result = _tokenService.ValidateTokenEncrypted(Token, out message, out token);

        if (result) return Ok(ApiResponses.Success(token));
        else return BadRequest();
    }
}
