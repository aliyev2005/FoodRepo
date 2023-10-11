using CryptoHelper;
using FoodProject.Data;
using FoodProject.DTO;
using FoodProject.Libraries.Repository;
using FoodProject.Model;
using Microsoft.AspNetCore.Mvc;

namespace FoodProject.Controllers.Authenitcation
{
    public class RegisterController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IFileManager _fileManager;
        private const string _PATH = "User";
        public RegisterController(ApplicationDbContext context, IFileManager fileManager)
        {
            _context = context;   
            _fileManager = fileManager;
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
                    PhoneNumber = request.PhoneNumber,
                    ProfilePicture = (request.ImageFile == null || request.ImageFile.Length == 0) ? "" : _fileManager.Upload(request.ImageFile, _PATH),
                    Password = Crypto.HashPassword(request.Password),  
                };
                _context.Users.Add(user);
                _context.SaveChanges();
                return Ok($"{request.Email} account has been succesfully registered.");
                //return Ok(user);
            }
            return BadRequest("An issue has arised, please contact admin....");
        }
    }
}
