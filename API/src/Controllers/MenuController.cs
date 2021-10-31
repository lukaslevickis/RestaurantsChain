using System.Threading.Tasks;
using API.DAL.Entities;
using API.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class MenuController : Controller
    {
        private readonly MenuService _menuService;

        public MenuController(MenuService menuService)
        {
            _menuService = menuService;
        }

        // GET: api/values
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok(await _menuService.GetAllAsync());
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            return Ok(await _menuService.GetByIdAsync(id));
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Menu menu)
        {
            return Ok(await _menuService.CreateAsync(menu));
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Menu menu)
        {
            if (id != menu.Id)//todo
            {
                return BadRequest();
            }

            return Ok(await _menuService.UpdateAsync(menu));
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _menuService.DeleteAsync(id);

            return NoContent();
        }
    }
}
