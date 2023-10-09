using CryptoHelper;
using FoodProject.Data;
using FoodProject.DTO;
using FoodProject.Model;
using Microsoft.AspNetCore.Mvc;

namespace FoodProject.Controllers.Authenitcation
{
    public class RegisterController : Controller
    {
        private readonly ApplicationDbContext _context;
        public RegisterController(ApplicationDbContext context)
        {
            _context = context;   
        }

        [HttpPost]
        [Route("/api/register/")]
        public IActionResult Index(RegisterRequest request)
        {
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
                return Ok($"{request.Email} account has been succesfully registered.");
            }
            return BadRequest("An issue has arised, please contact admin....");
        }
    }
}
