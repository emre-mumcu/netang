using Microsoft.AspNetCore.Mvc;

namespace Backend.App_Lib.Common;

[ApiController]
[Route("api/[controller]")]
[ServerInfoHeader]
public class MyControllerBase : ControllerBase
{

}