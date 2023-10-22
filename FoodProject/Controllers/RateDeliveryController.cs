using AutoMapper;
using FoodProject.Controllers.Authorization.Filter;
using FoodProject.Data;
using FoodProject.DTO;
using FoodProject.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Linq;
using System.Text.RegularExpressions;

namespace FoodProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RateDeliveryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private User _user => RouteData.Values["loggedUser"] as User;
        public RateDeliveryController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        [TypeFilter(typeof(UserAuthFilter))]
        public IActionResult GetGeneralInfo()
        {
            DeliveryInfoRequest request = new DeliveryInfoRequest();
            request.Store = _context.Stores.FirstOrDefault();
            if (_context.Reviews.Count() == 0)
            {
                return Ok("No ratings yet");
            }
            request.GeneralRating = _context.Reviews.ToList().Average(d => d.Rating);
            return Ok(request);
        }
        [HttpPost]
        [TypeFilter(typeof(UserAuthFilter))]
        [Route("/Rate")]
        public IActionResult RateAndReview([FromForm] RateAndReviewRequest request)
        {
            if (request.Rating == null || request.Rating < 1 || request.Rating > 5)
            {
                return BadRequest("Invalid Rating Value. Ratings should be between 1 and 5.");
            }
            Review review = new Review()
            {
                Rating = request.Rating,
                Title = request.Title,
                AddedDate = DateTime.Now,
                UserId = _user.Id
            };

            _context.Reviews.Add(review);
            _context.SaveChanges();

            return Ok("Review and Rating submitted successfully");
        }       
    }
}
