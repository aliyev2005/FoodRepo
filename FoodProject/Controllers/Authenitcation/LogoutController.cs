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

        //[TypeFilter(typeof(Auth))]
        [HttpPost]
        [Route("/api/logout")]
        public IActionResult Index()
        {
            User _user = RouteData.Values["loggedUser"] as User;
            if (_user != null)
            {
                _user.Token = null;
            }
            _context.SaveChanges();
            Response.Cookies.Delete("token");
            return Ok("Logged out");
            //return RedirectToAction("login", "admin");
        }
    }
}
