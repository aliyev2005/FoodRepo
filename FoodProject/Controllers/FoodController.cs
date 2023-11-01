using FoodProject.Controllers.Authorization.Filter;
using FoodProject.Data;
using FoodProject.DTO;
using FoodProject.Libraries.Repository;
using FoodProject.Model;
using Microsoft.AspNetCore.Mvc;

namespace FoodProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IFileManager _fileManager;
        private const string _PATH = "Food";
        public FoodController(ApplicationDbContext context, IFileManager fileManager)
        {
            _context = context;
            _fileManager = fileManager;
        }
        [HttpGet]
        [Route("Foods")]
        public IActionResult GetAll()
        {
            var foods = _context.Foods.ToList();
            return Ok(foods);
        }
        [HttpPost]
        [TypeFilter(typeof(UserAuthFilter))]
        [ServiceFilter(typeof(ApiKeyAuthFilter))]
        [Route("AddFood")]
        public IActionResult AddFood([FromForm]FoodAddRequest request)
        {
            #region Data Binding
            Food dataInsert = new()
            {
                Name = request.Name,
                ImageFileName = (request.ImageFile == null || request.ImageFile.Length == 0) ? "" : _fileManager.Upload(request.ImageFile, _PATH),
                Price = request.Price,
                StoreId = request.StoreId,
            };
            #endregion
            if (ModelState.IsValid)
            {
                _context.Add(dataInsert);
                _context.SaveChanges();
            }
            return Ok($"{dataInsert.Id} is added successfully");
        }
        [HttpDelete]
        [TypeFilter(typeof(UserAuthFilter))]
        [ServiceFilter(typeof(ApiKeyAuthFilter))]
        [Route("DeleteFood")]
        public IActionResult DeleteFood(Guid id)
        {
            Food? food = _context.Foods.Find(id);
            _fileManager.Delete(food.ImageFileName);
            _context.Foods.Remove(food);
            _context.SaveChanges();
            return Ok($"{id} - Deleted successfully");
        }          
    }
}
