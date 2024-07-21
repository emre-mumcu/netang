using Backend.App_Data;
using Backend.App_Lib;
using Backend.App_Lib.Common;
using Backend.App_Lib.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    public class CityController(AppDbContext dbContext) : MyControllerBase
    {
        bool useExecuter = true;

        [HttpGet("[action]")]
        public async Task<IActionResult> States()
        {
            if (useExecuter)
            {               
                var states = async () => await dbContext.States.ToListAsync();         
                var result = await new ExecuterService().ExecuterAsync(async () => await states());
                return Ok(ApiResponses.Success(result.Data, result.Elapsed));
            }
            else
            {
                var states = await dbContext.States.ToListAsync();
                return Ok(ApiResponses.Success(states));
            }
        }

        [HttpGet("[action]/{stateId:int}")]
        public async Task<IActionResult> Cities([FromRoute(Name = "stateId")] int id)
        {
            if(useExecuter)
            {
                var cities = async () => await dbContext.Cities.Where(c => c.StateId == id).ToListAsync();
                var result = await new ExecuterService().ExecuterAsync(async () => await cities());
                return Ok(ApiResponses.Success(result.Data, result.Elapsed));
            }
            else
            {
                var cities = await dbContext.Cities.ToListAsync();
                return Ok(ApiResponses.Success(cities));
            }
        }
    }
}