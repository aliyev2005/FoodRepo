﻿using FoodProject.Controllers.Authorization.Filter;
using FoodProject.Data;
using FoodProject.DTO;
using FoodProject.Libraries.Repository;
using FoodProject.Model;
using Microsoft.AspNetCore.Mvc;

namespace FoodProject.Controllers
{
    public class FoodController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IFileManager _fileManager;
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
            Food dataInsert = new();
            #region Data Binding
            dataInsert.Name = request.Name;
            dataInsert.ImageFileName = Upload(request.ImageFile);
            #endregion
            if (ModelState.IsValid)
            {
                _context.Add(dataInsert);
                _context.SaveChanges();
            }
            return Ok(request);
        }
        private string Upload(IFormFile file)
        {
            if (file == null) throw new Exception("Cannot upload null file");
            return _fileManager.Upload(file,"Food");
        }
    }
}
