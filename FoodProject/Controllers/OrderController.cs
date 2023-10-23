using AutoMapper;
using FoodProject.Controllers.Authorization.Filter;
using FoodProject.Data;
using FoodProject.DTO;
using FoodProject.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace FoodProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private User _user => RouteData.Values["loggedUser"] as User;
        public OrderController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;  
            _mapper = mapper;
        }
        [HttpGet]
        [TypeFilter(typeof(UserAuthFilter))]
        public IActionResult GetUpcomingOrders()
        {        
            var orders = _mapper.Map<List<GetOrderRequest>>(_context.Orders.Where(o => o.IsDelivered == false).Where(o => o.UserId == _user.Id).ToList());                      
            return Ok(orders);
        }
        //private int GetEstimatedArrival(int settedTime = 30, DateTime startTime)
        //{
        //    DateTime currentTime = DateTime.Now;
        //    var elapsedTime = TimeSpan.FromMinutes();
        //}
        [HttpPost]
        [TypeFilter(typeof(UserAuthFilter))]
        public IActionResult AddOrder([FromForm] AddOrderRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(request);
            }            
            Order order = new Order()
            {
                UserId = _user.Id,
                Food = _context.Foods.Where(f => f.Id == request.FoodId).FirstOrDefault(),
                Storeid = _context.Foods.Where(f => f.Id == request.FoodId).FirstOrDefault().StoreId,
                IsDelivered = false,
                OrderTime = DateTime.Now,
                EstimatedArrival = DateTime.Now.AddHours(2),
                Status = "Pending"
            };
            var totalPrice = order.Food.Price * order.FoodQuantity;
            order.TotalPrice = totalPrice;

            _context.Orders.Add(order);
            _context.SaveChanges();

            return Ok("Order added successfully");
        }
    }
}
