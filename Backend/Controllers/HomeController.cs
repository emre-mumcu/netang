using Microsoft.AspNetCore.Mvc;

namespace Backend;

public class HomeController : MyControllerBase
{
    [AddHeader("X-Server-Action", "Welcome")]
    [HttpGet("/api/welcome")]
    public IActionResult Welcome()
    {
        return Ok(ApiResults.Empty());
    }

    [HttpGet("")]
    public IActionResult GetData()
    {
        return Ok(ApiResults.Empty());
    }



}