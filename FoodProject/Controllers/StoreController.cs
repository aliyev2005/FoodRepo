using FoodProject.Controllers.Authorization.Filter;
using FoodProject.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace FoodProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public StoreController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        [TypeFilter(typeof(UserAuthFilter))]
        [Route("GetStores")]
        public IActionResult GetAllStores()
        {
            var stores = _context.Stores.Select(x => new {x.Id,x.Name,x.Logo}).ToList();
            return Ok(stores);
        }
    }
}
