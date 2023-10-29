using AutoMapper;
using FoodProject.Controllers.Authorization.Filter;
using FoodProject.Data;
using FoodProject.DTO;
using FoodProject.Model;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Buffers;
using Microsoft.AspNetCore.Server.HttpSys;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using Microsoft.VisualBasic;
using System.Reflection.Metadata.Ecma335;

namespace FoodProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private User _user => RouteData.Values["loggedUser"] as User;
        public OrderController(ApplicationDbContext context)
        {
            _context = context;  
        }
        [HttpGet]
        [TypeFilter(typeof(UserAuthFilter))]
        [Route("UpcomingOrders")]
        public IActionResult GetUpcomingOrders()
        {
            var ordersWithElapsedTime = _context.Orders
            .Where(o => o.UserId == _user.Id)
            .Where(o => o.EstimatedArrival >= DateTime.Now)
            .Select(o => new {
                Order = new
                {
                    Id = o.Id,
                    Food = new { o.Food.Name, o.Food.Id },
                    Store = new { o.Store.Id, o.Store.Name },
                    User = new
                    {
                       o.UserId,
                       o.User.Fullname,
                       o.User.Email,
                    },
                    o.EstimatedArrival,
                    IsDelivered = false,
                    o.Status,
                    o.FoodQuantity,
                     o.TotalPrice,
                },
                ElapsedTime = o.EstimatedArrival - DateTime.Now
            })
            .ToList();

            return Ok(ordersWithElapsedTime);
        }
        [HttpGet]
        [TypeFilter(typeof(UserAuthFilter))]
        [Route("OrderHistory")]
        public IActionResult GetOrdersHistory()
        {
            var orders = _context.Orders
                .Where(o => o.EstimatedArrival <= DateTime.Now)
                .Where(o => o.UserId == _user.Id)
                .Select(o => new {
                    o.Id,
                    Store = new {
                        o.Storeid,
                        o.Store.Name
                    },
                    User = new { 
                        o.UserId, 
                        o.User.Fullname,
                        o.User.Email 
                    },
                    o.FoodQuantity,
                    o.TotalPrice,
                    o.Food.Name,
                    IsDelivered = true,
                    o.OrderTime
                }).ToList();

            return Ok(orders);
        }
        [HttpPost]
        [TypeFilter(typeof(UserAuthFilter))]
        public IActionResult AddOrder([FromForm] AddOrderRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(request);
            }
            var food = _context.Foods.Where(f => f.Id == request.FoodId).FirstOrDefault();

            Order order = new Order()
            {
                UserId = _user.Id,
                Food = _context.Foods.Where(f => f.Id == request.FoodId).FirstOrDefault(),
                Storeid = _context.Foods.Where(f => f.Id == request.FoodId).FirstOrDefault().StoreId,
                OrderTime = DateTime.Now,
                EstimatedArrival = DateTime.Now.AddHours(1),
                Status = "Pending",
                FoodQuantity = request.Quantity,
                TotalPrice = food.Price * request.Quantity,
            };

            _context.Orders.Add(order);
            _context.SaveChanges();

            return Ok("Order added successfully");
        }
        [HttpDelete]
        [TypeFilter(typeof(UserAuthFilter))]
        [Route("DeleteOrder")]
        public IActionResult DeleteOrder(Guid id)
        {
            var order = _context.Orders.FirstOrDefault(o => o.Id == id);
           
            _context.Orders.Remove(order);  
            _context.SaveChanges();
            
            return Ok("Order deleted successfully");
        }
    }
}
