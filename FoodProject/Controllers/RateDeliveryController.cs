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
            RateDeliveryRequest request = new RateDeliveryRequest();
            request.Store = _context.Stores.FirstOrDefault();
            if (_context.Deliveries.Count() == 0)
            {
                return Ok("No ratings yet");
            }
            request.GeneralRating = _context.Deliveries.ToList().Average(d => d.Rating);
            return Ok(request);
        }
        [HttpPost]
        [TypeFilter(typeof(UserAuthFilter))]
        [Route("/Rate")]
        public IActionResult RateDelivery([FromForm] double rating)
        {
            if(rating == null || rating < 1 || rating > 5)
            {
                return BadRequest("Invalid Rating Value. Ratings should be between 1 and 5.");
            }
            Delivery delivery = new Delivery()
            { 
                Rating = rating
            };
            _context.Deliveries.Add(delivery);
            _context.SaveChanges();
            return Ok("Rating submitted successfully");
        }
        
        [TypeFilter(typeof(UserAuthFilter))]
        [HttpPost]
        [Route("/Review")]
        public IActionResult CreateReview([FromForm]Review review)
        {
            review.AddedDate = DateTime.Now;
            review.UserId = _user.Id;

            _context.Reviews.Add(review);
            _context.SaveChanges();
            return Ok(review);
        }
    }
}
