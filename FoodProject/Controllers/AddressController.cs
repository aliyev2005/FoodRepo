using FoodProject.Controllers.Authorization.Filter;
using FoodProject.Data;
using FoodProject.DTO;
using FoodProject.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace FoodProject.Controllers
{
    public class AddressController : Controller
    {
        private readonly ApplicationDbContext _context;
        public AddressController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        [TypeFilter(typeof(UserAuthFilter))]
        [Route("/api/address")]
        public IActionResult GetUserAddresses()
        {
            var data = _context.Adresses.
                Where(
                    u => u.UserId == ReturnUserData().Id
                ).ToList()
                .Select(d => new AddressRequest
                {
                    City = d.City,
                    Street = d.Street,
                }).ToList();
            if (data.IsNullOrEmpty())
            {
                return Ok("No addresses found");
            }      
            return Ok(data);
        }
        [HttpPost]
        [TypeFilter(typeof(UserAuthFilter))]
        [Route("/api/address")]
        public IActionResult AddAddress(AddressRequest request)
        {
            UserAdress address = new()
            {
                City = request.City,
                Street = request.Street,
                UserId = ReturnUserData().Id,

            };
            if (ModelState.IsValid)
            {
                _context.Add(address);
                _context.SaveChanges();
                return Ok($"Address - {address.Id} has been added to database");
            }
            return BadRequest("Something went wrong, please contact administrator.");
           
        }
        private User? ReturnUserData()
        {
            return _context.Users.Where(u => u.Token == Request.Cookies["token"]).First();
        }
    }
}
