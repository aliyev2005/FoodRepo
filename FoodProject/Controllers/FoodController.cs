using FoodProject.Data;
using Microsoft.AspNetCore.Mvc;

namespace FoodProject.Controllers
{
    public class FoodController : Controller
    {
        private readonly ApplicationDbContext _context;
        public FoodController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("/api/food")]
        public IActionResult Index()
        {
            var food = _context.Foods.ToList();
            return Ok(food);
        }
    }
}
