using NetCoreApiBoilerplate.Application.Samples;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using NetCoreApiBoilerplate.Areas.Auth.Models;

namespace NetCoreApiBoilerplate.Application.Auth
{
    public class CreateUserRequest : IRequest<CreateUserResult>
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }

    public class CreateUserResult
    {
        public string ResultText { get; set; }
        public IdentityResult Result { get; set; }
    }

    public class CreateUserHandler : IRequestHandler<CreateUserRequest, CreateUserResult>
    {
        private readonly UserManager<ApiUser> _userManager;
        private readonly SignInManager<ApiUser> _signInManager;

        public CreateUserHandler(UserManager<ApiUser> userManager, SignInManager<ApiUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<CreateUserResult> Handle(CreateUserRequest request, CancellationToken cancellationToken)
        {
            var result = await _userManager.CreateAsync(new ApiUser { UserName = request.Username, Email = request.Email }, request.Password);
            var user = await _userManager.FindByEmailAsync(request.Email);
            
            return new CreateUserResult{
                ResultText = "Success",
                Result = result
            };
        }
    }
}