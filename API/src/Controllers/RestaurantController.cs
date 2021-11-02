using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DAL.Entities;
using API.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class RestaurantController : Controller
    {
        private readonly RestaurantService _restaurantService;

        public RestaurantController(RestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        // GET: api/values
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok(await _restaurantService.GetAllAsync());
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            return Ok(await _restaurantService.GetByIdAsync(id));
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Restaurant restaurant)
        {
            return Ok(await _restaurantService.CreateAsync(restaurant));
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Restaurant restaurant)
        {
            if (id != restaurant.Id)//todo
            {
                return BadRequest();
            }

            return Ok(await _restaurantService.UpdateAsync(restaurant));
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _restaurantService.DeleteAsync(id);

            return NoContent();
        }

        [HttpGet]
        [Route("menu/{id}")]
        public async Task<ActionResult> GetRestaurantsByMeal(int id)
        {
            return Ok(await _restaurantService.GetRestaurantsByMealAsync(id));
        }
    }
}
