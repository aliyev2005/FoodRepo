using FoodProject.Data;
using FoodProject.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FoodProject.Controllers.Authorization.Filter
{
    public class UserAuthenticationAuth: ActionFilterAttribute
    {
        private readonly ApplicationDbContext _context;
        public UserAuthenticationAuth(ApplicationDbContext context)
        {
            _context = context;
        }
        //Using this system allows others to hijack tokens. Meaning if I use the token of someone else I can login without requiring password or email.
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.HttpContext.Request.Cookies.TryGetValue("token", out string token))
            {
                context.Result = new UnauthorizedObjectResult("User is not logged in.");
            }
            User user = _context.Users.FirstOrDefault(u => u.Token == token);
            if (user == null)
            {
                context.Result = new UnauthorizedObjectResult("User cannot be found.");
            }
            if (context.Controller is Controller controller)
            {
                controller.ViewBag.User = user;
            }
            context.RouteData.Values["loggedUser"] = user;
        }
    }
}
