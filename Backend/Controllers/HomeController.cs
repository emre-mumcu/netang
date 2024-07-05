using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend;

[AddHeader("X-Server-Controller", "Home")]
public partial class HomeController : MyControllerBase
{
    [AddHeader("X-Server-Action", "Welcome")]
    [HttpGet("/")] // /
    [HttpGet("/api/welcome")] // /api/welcome
    public IActionResult Welcome() => Ok(ApiResponses.Success());

    [HttpGet("[action]/{id:int}")] // /api/home/getdata1/111
    public IActionResult GetData1(int id) => Ok(ApiResponses.Success(id));

    [HttpGet("[action]/{id:int}")] // /api/home/getdata2/222
    public IActionResult GetData2([FromRoute(Name = "id")] int UserId) => Ok(ApiResponses.Success(UserId));

    [HttpGet("[action]")] // /api/home/getdata3/?id=333
    public IActionResult GetData3([FromQuery(Name = "id")] int UserId) => Ok(ApiResponses.Success(UserId));
}

public partial class HomeController: MyControllerBase
{
    [HttpPost("[action]")] // /api/home/post1
    public IActionResult Post1() => Ok(ApiResponses.Success());

    [HttpPost("[action]/{id:int}")] // /api/home/post2/222
    public IActionResult Post2(int id) => Ok(ApiResponses.Success(id));

    [HttpPost("[action]/{id:int}")] // /api/home/post3/333
    public IActionResult Post3([FromRoute(Name = "id")] int UserId) => Ok(ApiResponses.Success(UserId));

    [HttpPost("[action]")] // /api/home/post1
    public IActionResult Post4(UserDto user) => Ok(ApiResponses.Success(user));

}

public partial class HomeController : MyControllerBase
{
    


    [Authorize]
    [HttpGet("/api/admin")] // /api/admin
    public IActionResult Admin() => Ok(ApiResponses.Success());
}


public class UserDto {
    public int UserId { get; set; }
    public string UserName { get; set; } = null!;
}