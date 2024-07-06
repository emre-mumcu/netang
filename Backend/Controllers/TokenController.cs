using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Backend.App_Lib;
using Backend.AppLib.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Backend;

public class Sample {
    public int MyProperty1 { get; set; } 
    public string? MyProperty2 { get; set; }
    public DateTime MyProperty3 { get; set; }

    public static Sample Instance()
    {
        Sample s = new Sample();

        s.MyProperty1 = 100;
        s.MyProperty2 = "Deneme 123";
        s.MyProperty3 = DateTime.Now;

        return s;

    }
}

public class TokenController : MyControllerBase
{


    [HttpGet("[action]")]
    public IActionResult action1()
    {
        return Ok(Sample.Instance());
    }

    [HttpGet("[action]")]
    public IActionResult action2()
    {
        return BadRequest(Sample.Instance());
    }

    [HttpGet("[action]")]
    public IResult action3()
    {
        return Results.Json(Sample.Instance());
    }

    [HttpGet("[action]")]
    public IActionResult action4()
    {
        return new JsonResult(Sample.Instance());
    }


    private readonly ITokenService _tokenService;

    public TokenController(ITokenService tokenService) { _tokenService = tokenService; }

    [HttpGet("[action]")]
    public IActionResult Create()
    {
        var userClaims = new List<Claim>{
            new Claim(JwtRegisteredClaimNames.NameId, "12345"),
            new Claim(JwtRegisteredClaimNames.GivenName, "emre-mumcu")
        };

        string token = _tokenService.CreateToken(new ClaimsIdentity(userClaims));


        return  Ok(ApiResponses.Success(token));
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
