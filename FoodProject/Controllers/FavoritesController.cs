using FoodProject.Controllers.Authorization.Filter;
using FoodProject.Data;
using FoodProject.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace FoodProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoritesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public FavoritesController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        //Favorite Food
        [HttpPost]
        [TypeFilter(typeof(UserAuthFilter))]
        [Route("ToggleFavorite/{id}")]
        public IActionResult ToggleFavoriteFood(Guid id)
        {
            var food = _context.Foods.FirstOrDefault(f => f.Id == id);
            if (food == null)
                return NotFound();
            if(food.IsFavorited == true)
                return BadRequest($"{food.Name} is already favorited");

            food.IsFavorited = true;
            _context.Foods.Update(food);
            _context.SaveChanges();

            return Ok($"{food.Name} added to favorites");
        }
        [HttpPost]
        [TypeFilter(typeof(UserAuthFilter))]
        [Route("RemoveFavFood/{id}")]
        public IActionResult RemoveFromFavFood(Guid id)
        {
            var food = _context.Foods.FirstOrDefault(f => f.Id == id);
            if (food == null) 
                return NotFound();
            if(food.IsFavorited == false)
                return BadRequest($"{food.Name} is not in favorites");

            food.IsFavorited = false;
            _context.Foods.Update(food);
            _context.SaveChanges();

            return Ok($"{food.Name} removed from favorites");
        }

        [HttpGet]
        [TypeFilter(typeof(UserAuthFilter))]
        [Route("Foods")]
        public IActionResult GetFavoriteFoods()
        {
            var favorites = _context.Foods.Where(f => f.IsFavorited == true).ToList();
            return Ok(favorites);
        }
        //Favorite Stores
        [HttpPost]
        [TypeFilter(typeof(UserAuthFilter))]
        [Route("Stores/{id}")]
        public IActionResult ToggleFavoriteStore(Guid id)
        {
            var store = _context.Stores.FirstOrDefault(f => f.Id == id);
            if(store == null)
            {
                return NotFound();
            }
            if (store.IsFavorited == true)
                return BadRequest($"{store.Name} is already favorited");

            store.IsFavorited = true;

            _context.Stores.Update(store);
            _context.SaveChanges();
            return Ok($"{store.Name} added favorites");
        }
        [HttpPost]
        [TypeFilter(typeof(UserAuthFilter))]
        [Route("RemoveFavStore/{id}")]
        public IActionResult RemoveFromFavoriteStores(Guid id)
        {
            var store = _context.Stores.FirstOrDefault(store => store.Id == id);   
            if(store == null)
                return NotFound();
            if(store.IsFavorited) 
                return BadRequest($"{store.Name} is not in favorite stores");

            store.IsFavorited = false;
            _context.Stores.Update(store);
            _context.SaveChanges();

            return Ok($"{store.Name} removed from Favorites");
        }

        [HttpGet]
        [TypeFilter(typeof(UserAuthFilter))]
        [Route("Stores")]
        public IActionResult GetFavoriteStores()
        {
            var stores = _context.Stores.Where(store => store.IsFavorited == true).Select(
                store => new
                {
                    store.Id,
                    store.Name,
                    store.Logo,
                    store.Foods
                }); 
            return Ok(stores);
        }
    }
}
