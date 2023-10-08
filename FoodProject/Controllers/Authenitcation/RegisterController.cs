using CryptoHelper;
using FoodProject.Data;
using FoodProject.DTO;
using FoodProject.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodProject.Controllers.Authenitcation
{
    public class RegisterController : Controller
    {
        private readonly ApplicationDbContext _context;
        public RegisterController(ApplicationDbContext context)
        {
            _context = context;   
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}
        [HttpPost]
        public IActionResult Index(RegisterRequest request)
        {
            Console.WriteLine("Create method working!");
            if (_context.Users.Any(u => u.Email == request.Email))
            {
                ModelState.AddModelError("Email", "The email is already in use");
            }
            if (ModelState.IsValid)
            {
                User user = new()
                {
                    Email = request.Email,
                    Fullname = request.Fullname,
                    Password = Crypto.HashPassword(request.Password),  
                };
                _context.Users.Add(user);
                _context.SaveChanges();
                return RedirectToAction("index", "admin");
            }
            return View(request);
        }
    }
}
