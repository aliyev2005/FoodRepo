using FoodProject.Controllers.Authorization.Filter;
using FoodProject.Data;
using FoodProject.DTO;
using FoodProject.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
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
            var stores = _context.Stores.Select(x => new { x.Id, x.Name, x.Logo }).ToList();
            return Ok(stores);
        }
        [HttpPost]
        [TypeFilter(typeof(UserAuthFilter))]
        [Route("Add")]
        public IActionResult AddStore([FromForm] AddStoreRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Store store = new Store()
            {
                Name = request.Name,
                Logo = request.Logo,
                Address = request.Address,
                IsFavorited = false,
                BackImage = request.BackImage,
            };

            _context.Stores.Add(store);
            _context.SaveChanges();

            return Ok($"{store.Name} is added");
        }
        [HttpGet]
        [TypeFilter(typeof(UserAuthFilter))]
        [Route("Details/{id}")]
        public IActionResult Details(Guid id)
        {
            var store = _context.Stores.Include(sto => sto.Foods).FirstOrDefault(s => s.Id == id);
            if (store == null)
                return NotFound();

            var storeWithFoods = new
            {
                store.Id,
                store.Name,
                store.Logo,
                store.Address,
                store.BackImage,
                Foods = store.Foods.Where(f => f.StoreId == store.Id).Select(food => new
                {
                    food.Id,
                    food.Name,
                    food.Price,
                    food.ImageFileName
                })
            };

            return Ok(storeWithFoods);
        }

    }
}
