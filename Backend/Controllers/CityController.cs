using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using Backend.App_Data;
using Backend.App_Data.Entities;
using Backend.App_Lib;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace Backend.Controllers
{
    public class CityController(AppDbContext dbContext) : MyControllerBase
    {
        [HttpGet("[action]")]
        public async Task<IActionResult> GetStates()
        {
            var states = async () => await dbContext.States.ToListAsync();

            var result = await ExecuterAsync(async () => await states());

            return Ok(ApiResponses.Success(result.Data, result.Elapsed));
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetCities()
        {
            var cities = await dbContext.Cities.ToListAsync();

            return Ok(ApiResponses.Success(cities));
        }

        [HttpGet("[action]/{id:int}")]
        public async Task<IActionResult> GetCitiesByStateId(int id)
        {
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();

            var cities = await dbContext.Cities.Where(c => c.StateId == id).ToListAsync();

            stopwatch.Stop();

            return Ok(ApiResponses.Success(cities, stopwatch.ElapsedMilliseconds));
        }

        private long Executer(Action action)
        {
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();

            action.Invoke();

            stopwatch.Stop();

            return stopwatch.ElapsedMilliseconds;
        }

        private (T? Data, long Elapsed) Executer<T>(Func<T?> func)
        {
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();

            var result = func.Invoke();

            stopwatch.Stop();

            return (result, stopwatch.ElapsedMilliseconds);
        }

        private async Task<(T? Data, long Elapsed)> ExecuterAsync<T>(Func<Task<T?>> func)
        {
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();

            var result = await func();

            stopwatch.Stop();

            return (result, stopwatch.ElapsedMilliseconds);
        }
    }
}