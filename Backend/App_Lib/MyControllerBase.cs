using Microsoft.AspNetCore.Mvc;

namespace Backend;

[ApiController]
[Route("api/[controller]")]
[ServerInfoHeader]
public class MyControllerBase : ControllerBase
{

}