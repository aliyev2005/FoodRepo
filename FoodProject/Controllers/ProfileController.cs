using AutoMapper;
using FoodProject.Controllers.Authorization.Filter;
using FoodProject.Data;
using FoodProject.DTO;
using FoodProject.Libraries.Repository;
using FoodProject.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private User _user => RouteData.Values["loggedUser"] as User;   
        private readonly ApplicationDbContext  _context;
        private readonly IMapper _mapper;
        private readonly IFileManager _fileManager;
        private const string _PATH = "Food";
        public ProfileController(ApplicationDbContext context,IMapper mapper, IFileManager fileManager)
        {
            _mapper = mapper;
            _context = context;
            _fileManager = fileManager;
        }
        [HttpGet]
        [TypeFilter(typeof(UserAuthFilter))]
        [Route("/Profile")]
        public IActionResult Index()
        {
            var user = _mapper.Map<ProfileRequest>(_context.Users.Where(u => u.Id == _user.Id).FirstOrDefault());
            return Ok(user);
        }
        [HttpPut]
        [Route("edit/")]
        [TypeFilter(typeof(UserAuthFilter))]
        public IActionResult Edit([FromForm]ProfileRequest request)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest("Please enter all information");
            }
            var user = _context.Users.Where(u => u.Id == _user.Id).FirstOrDefault();
            if(user == null)
            {
                return NotFound("This user doesn't exists");
            }
            user.Fullname = request.Fullname;
            user.PhoneNumber = request.PhoneNumber;
            user.Email = request.Email;
            user.ProfilePicture = (request.ImageFile == null || request.ImageFile.Length == 0) ? "" : _fileManager.Upload(request.ImageFile, _PATH);
            
            _context.Users.Update(user);
            _context.SaveChanges();
            return Ok($"{user} - Successfully Updated");
        }
    }
}
