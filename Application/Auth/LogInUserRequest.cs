using Azure;
using NetCoreApiBoilerplate.Application.Samples;
using MediatR;
using Microsoft.AspNetCore.Identity;
using NetCoreApiBoilerplate.Areas.Auth.Models;

namespace NetCoreApiBoilerplate.Application.Auth
{
    public class LogInUserRequest : IRequest<LogInUserResult>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LogInUserResult
    {
        public ApiUser User { get; set; }
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }

    public class LogInRequestHandler : IRequestHandler<LogInUserRequest, LogInUserResult>
    {
        private readonly UserManager<ApiUser> _userManager;
        private readonly SignInManager<ApiUser> _signInManager;
        private readonly TokenService _tokenService;
        public LogInRequestHandler(SignInManager<ApiUser> signInManager, UserManager<ApiUser> userManager, TokenService tokenService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenService = tokenService;
        }
        public async Task<LogInUserResult> Handle(LogInUserRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null) return null;

            var res = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            
            if (res?.Succeeded ?? false)
            {
                return new LogInUserResult
                {
                    User = user,
                    Token = _tokenService.CreateToken(user),
                };
            }
            return null;
        }

       
    }

}
