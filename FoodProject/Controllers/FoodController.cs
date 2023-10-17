﻿using FoodProject.Controllers.Authorization.Filter;
using FoodProject.Data;
using FoodProject.DTO;
using FoodProject.Libraries.Repository;
using FoodProject.Model;
using Microsoft.AspNetCore.Mvc;

namespace FoodProject.Controllers
{
    [Route("/api/food")]
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
        public IActionResult GetAll()
        {
            var food = _context.Foods.ToList();
            return Ok(food);
        }
        [HttpPost]
        [TypeFilter(typeof(UserAuthFilter))]
        [ServiceFilter(typeof(ApiKeyAuthFilter))]
        public IActionResult AddFood([FromForm]FoodAddRequest request)
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
        [HttpDelete]
        [TypeFilter(typeof(UserAuthFilter))]
        [ServiceFilter(typeof(ApiKeyAuthFilter))]
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
