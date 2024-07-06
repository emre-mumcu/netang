# http REST-Client Sample

```zsh
@approot = http://localhost:5555

GET {{approot}}/api/welcome HTTP/1.1
# [HttpGet("/api/welcome")]
# public IActionResult Welcome()
###

GET {{approot}}/api/home/getdata1/111
# [HttpGet("[action]/{id:int}")]
# public IActionResult GetData1(int id)
###

GET {{approot}}/api/home/getdata2/222
# [HttpGet("[action]/{id:int}")]    
#  public IActionResult GetData2([FromRoute(Name = "id")] int UserId)
###

GET {{approot}}/api/home/getdata3/?id=333
# [HttpGet("[action]")]
# public IActionResult GetData3([FromQuery(Name = "id")] int UserId)
###

POST {{approot}}/api/home/post1
# [HttpPost("[action]")] 
# public IActionResult Post1()
###

POST {{approot}}/api/home/post2/222
# [HttpPost("[action]/{id:int}")]     
# public IActionResult Post2(int id)
###

POST {{approot}}/api/home/post3/333
# [HttpPost("[action]/{id:int}")]
# public IActionResult Post3([FromRoute(Name = "id")] int UserId)
###

POST {{approot}}/api/home/post4
content-type: application/json

{
    "userId": 1,
    "userName": "Emre"
}

# [HttpPost("[action]")]
# public IActionResult Post4(UserDto user) 
# json data key names are NOT case-sensitive
###

GET {{approot}}/api/token/create HTTP/1.1
###

GET {{approot}}/api/admin HTTP/1.1
Authorization: bearer eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIxIiwiZ2l2ZW5fbmFtZSI6IkVtcmUiLCJuYmYiOjE3MTk3Mzc0MzUsImV4cCI6MTcyMDM0MjIzNSwiaWF0IjoxNzE5NzM3NDM1LCJpc3MiOiJOZXRBbmQiLCJhdWQiOiIqIn0.xhGCwYKrl9HshgQImL7YqJ7Mj3yuLpGS1iFoNh6K1w7oC97nvhWgljGXutuxBSD4SFxMJyYZIl-Q47F2jOxvDg
###



authorization: Basic someuser somekey
content-type: application/x-www-form-urlencoded
```

# Home Controller

```cs
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
```

/*

dotnet add package log4net
[optional]
dotnet add package log4net.Ext.Json

git config --global user.name "Emre Mumcu"
git config --global user.email "emre@mumcu.net"
git config --global http.sslVerify false
git config --global init.defaultBranch <name>

git init
git init -b main
git add -A
git commit -m "First Commit"
git branch -M maian
git remote add origin https://github.com/emre-mumcu/netang.git
git remote -v
git push -u origin main

git add -A
git commit -m “Second Commit”
git push -u origin main
*/