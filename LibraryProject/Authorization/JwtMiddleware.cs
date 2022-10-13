using LibraryProject.API.Helpers;
using LibraryProject.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryProject.API.Authorization
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings _appSettings;

        public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings)
        {
            _next = next;
            _appSettings = appSettings.Value;
        }

        public async Task Invoke(HttpContext context, IUserService UsersService, IJwtUtils jwtUtils)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var UsersId = jwtUtils.ValidateJwtToken(token);
            if (UsersId != null)
            {
                // attach customer to context on successful jwt validation
                context.Items["Customer"] = await UsersService.GetById(UsersId.Value);
            }

            await _next(context);
        }
    }
}
