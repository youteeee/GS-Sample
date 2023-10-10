using GS_Sample.Data;
using GS_Sample.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GS_Sample.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarsController : Controller
    {
        private readonly CarsApiDbContext dbContext;

        public CarsController(CarsApiDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetCars()
        {
            return Ok(await dbContext.Cars.ToListAsync());
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetCar([FromRoute] Guid id)
        {
            var car = await dbContext.Cars.FindAsync(id);
            if(car == null)
            {
                return NotFound();
            }
            return Ok(car);
        }

        [HttpPost]
        public async Task<IActionResult> AddCar(AddCarRequest addCarRequest)
        {
                var cars = new Car()
                {
                    Id = Guid.NewGuid(),
                    Make = addCarRequest.Make,
                    Model = addCarRequest.Model,
                    Year = addCarRequest.Year

                };

                await dbContext.Cars.AddAsync(cars);
                await dbContext.SaveChangesAsync();

                return Ok(cars);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateCar([FromRoute] Guid id, UpdateCarRequest updateCarRequest)
        {
            var car = await dbContext.Cars.FindAsync(id);
            if (car != null)
            {
                car.Make = updateCarRequest.Make;
                car.Model = updateCarRequest.Model;
                car.Year = updateCarRequest.Year;

                await dbContext.SaveChangesAsync();
                return Ok(car);
            }
            return NotFound();
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var car = await dbContext.Cars.FindAsync(id);
            if (car != null)
            {
                dbContext.Remove(car);
                await dbContext.SaveChangesAsync();
                return Ok(car);
            }
            return NotFound();
        }
    }
}
