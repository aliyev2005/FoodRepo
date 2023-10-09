using CryptoHelper;
using FoodProject.Data;
using FoodProject.DTO;
using FoodProject.Model;
using Microsoft.AspNetCore.Mvc;

namespace FoodProject.Controllers.Authenitcation
{
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LoginController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("/api/login/")]
        public IActionResult Index(LoginRequest request)
        {
            if (ModelState.IsValid)
            {
                User user = _context.Users.FirstOrDefault(u => u.Email == request.Email);
                if (user != null && Crypto.VerifyHashedPassword(user.Password, request.Password) && user.LoginFails < 3)
                {
                    user.Token = Guid.NewGuid().ToString();
                    user.LoginFails = 0;
                    _context.SaveChanges();
                    Response.Cookies.Append("token", user.Token, new CookieOptions
                    {
                        Expires = request.RememberUser ? DateTimeOffset.Now.AddDays(24) : null,
                        HttpOnly = true
                    });
                    return Ok($"Login succesful - {request.Email}");
                }
                if (user.LoginFails >= 3)
                {
                    ModelState.AddModelError("Password", $"Account locked due to {user.LoginFails} failed login attemps. Please contact administrator.");
                }
                ModelState.AddModelError("Password", "Email or Password Is Incorrect. Check the details.");
                user.LoginFails++;
                _context.SaveChanges();
            }
            return BadRequest("An issue has arised, please contact admin....");
        }
    }
}
