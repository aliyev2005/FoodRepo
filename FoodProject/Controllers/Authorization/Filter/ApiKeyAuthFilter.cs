using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FoodProject.Controllers.Authorization.Filter
{
    public class ApiKeyAuthFilter : IAuthorizationFilter
    {
        private readonly IConfiguration _configuration;

        public ApiKeyAuthFilter(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue(AuthConstants.ApiKeyHeaderName,
                out var extractedKey))
            {
                context.Result = new UnauthorizedObjectResult("API Key missing");
                return;
            }
            string? apiKey = _configuration.GetValue<string>(AuthConstants.ApiSectionName);
            if (!apiKey.Equals(extractedKey))
            {
                context.Result = new UnauthorizedObjectResult("API Key invalid");
                return;
            }
        }
    }
}
