using AutoMapper;
using FoodProject.Controllers.Authorization.Filter;
using FoodProject.Data;
using FoodProject.DTO;
using FoodProject.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Validations;

namespace FoodProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private User _user => RouteData.Values["loggedUser"] as User;
        public ReviewsController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
        }
        [HttpGet]
        [TypeFilter(typeof(UserAuthFilter))]
        public IActionResult GetAllReviews()
        {
            var reviews = _context.Reviews.Select(
                c => new
                {
                    c.Id,
                    c.Rating,
                    c.Title,
                    c.AddedDate,
                    c.User.Fullname,
                    c.UserId,
                }                
                ).OrderByDescending(r => r.AddedDate).ToList();
            return Ok(reviews);
        }
    }
}
