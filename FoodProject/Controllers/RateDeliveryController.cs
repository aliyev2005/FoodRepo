using FoodProject.Controllers.Authorization.Filter;
using FoodProject.Data;
using FoodProject.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Text.RegularExpressions;

namespace FoodProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RateDeliveryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private User _user => RouteData.Values["loggedUser"] as User;
        [HttpGet]
        public IActionResult Rate()
        {
            return Ok();
        }
        
        
        [TypeFilter(typeof(UserAuthFilter))]
        [HttpPost]
        public IActionResult CreateReview(Review review)
        {
            review.AddedDate = DateTime.Now;
            review.User = _user;

            _context.Reviews.Add(review);
            _context.SaveChanges();
            return Ok(review);
        }
    }
}
