using FoodProject.Data;
using FoodProject.Model;
using Microsoft.AspNetCore.Mvc;

namespace FoodProject.Controllers.Authenitcation
{
    public class LogoutController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LogoutController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("/api/logout")]
        public IActionResult Index()
        {
            if (RouteData.Values["loggedUser"] is User _user)
            {
                _user.Token = null;
            }
            _context.SaveChanges();
            Response.Cookies.Delete("token");
            return Ok("Logged out");
        }
    }
}
