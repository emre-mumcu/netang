using Microsoft.AspNetCore.Mvc;

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
        UserDto userDto = new UserDto { UserId = 1, UserName = "Emre" };

        string token = _tokenService.CreateToken(userDto);

        return Ok(ApiResults.Success(token));
    }
}
