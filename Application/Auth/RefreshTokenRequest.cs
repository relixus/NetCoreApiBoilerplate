using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NetCoreApiBoilerplate.Areas.Auth.Models;

namespace NetCoreApiBoilerplate.Application.Auth
{
    public class RefreshTokenRequest : IRequest<RefreshTokenResult>
    {
        public string Id { get; set; }
    }

    public class RefreshTokenResult
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }

    public class RefreshTokenRequestHandler : IRequestHandler<RefreshTokenRequest, RefreshTokenResult>
    {
        private readonly SignInManager<ApiUser> _signInManager;
        private readonly UserManager<ApiUser> _userManager;
        private readonly TokenService _tokenService;

        public RefreshTokenRequestHandler(SignInManager<ApiUser> signInManager, UserManager<ApiUser> userManager, TokenService tokenService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public async Task<RefreshTokenResult> Handle(RefreshTokenRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.Id) ?? throw new InvalidOperationException();
           
           // await _signInManager.RefreshSignInAsync(user);

            var newacctoken = _tokenService.CreateToken(user);
            var newreftoken = _tokenService.GenerateRefreshToken();

            return new RefreshTokenResult
            {
                AccessToken = newacctoken,
                RefreshToken = newreftoken.Token,
            };
        }
    }
}
