using FoodProject.Controllers.Authorization.Filter;
using FoodProject.Data;
using FoodProject.DTO;
using FoodProject.Libraries;
using FoodProject.Libraries.Repository;
using FoodProject.Model;
using Microsoft.AspNetCore.Mvc;

namespace FoodProject.Controllers
{
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
        [TypeFilter(typeof(UserAuthFilter))]
        [ServiceFilter(typeof(ApiKeyAuthFilter))]
        [Route("/api/food")]
        public IActionResult GetAll()
        {
            var food = _context.Foods.ToList();
            return Ok(food);
        }
        [HttpPost]
        [TypeFilter(typeof(UserAuthFilter))]
        [ServiceFilter(typeof(ApiKeyAuthFilter))]
        [Route("/api/food")]
        public IActionResult AddFood(FoodAddRequest request)
        {
            #region Data Binding
            Food dataInsert = new()
            {
                Name = request.Name,
                ImageFileName = (request.ImageFile == null || request.ImageFile.Length == 0) ? "" : _fileManager.Upload(request.ImageFile, _PATH),
            };
            #endregion
            if (ModelState.IsValid)
            {
                _context.Add(dataInsert);
                _context.SaveChanges();
            }
            return Ok(request);
        }
    }
}
