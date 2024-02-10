using NetCoreApiBoilerplate.Mediators.Auth;
using NetCoreApiBoilerplate.Mediators.Samples;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetCoreApiBoilerplate.BLL.Areas.Auth.Models;
using NetCoreApiBoilerplate.Mediators.Auth;
using NetCoreApiBoilerplate.BLL.Areas.Common.Models;
using NetCoreApiBoilerplate.Middlewares.CustomAuthorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using NetCoreApiBoilerplate.Areas;
using Microsoft.AspNetCore.Authorization;

namespace DotnetApiBoilerplate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ApiController
    {
        private readonly UnitOfWork unitOfWork;

        public AuthController(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost("create-user")]
        public async Task<ActionResult<CreateUserResult>> GetSampleRequest([FromBody] CreateUserRequest request)
        {
            return await Mediator.Send(request);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LogIn([FromBody] LogInUserRequest request)
        {
            var user = await Mediator.Send(request);
            if (user == null)
            {
                return NotFound(request);
            }
            return Ok(user);
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost("refresh-token")]
        public async Task<RefreshTokenResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            return await Mediator.Send(request);
        }



        [HttpPost("create-claim-template")]
        [RequireClaim("Auth", "ManageUsers")]
        public async Task<ActionResult<UserClaimsTemplate>> CreateClaimTemplate([FromBody] CreateClaimTemplateRequest request)
        {
            return await Mediator.Send(request);
        }

        [HttpPost("set-user-claims")]
        [RequireClaim("Auth", "ManageUsers")]
        public async Task<ActionResult> SetUserClaims([FromBody] SetUserClaimsRequest request)
        {
            await Mediator.Send(request);
            return Ok();
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("get-user-info")]
        [RequireClaim("Auth", "ManageUsers")]
        public async Task<ActionResult<UserInfoResource>> GetUserInfo([FromQuery] GetUserInfoRequest request)
        {
            return await Mediator.Send(request);
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost("admin-reset-password")]
        [RequireClaim("Auth", "ManageUsers")]
        public async Task<ActionResult<OperationResultResource>> AdminResetPassword([FromBody] AdminResetPasswordRequest request)
        {
            return await Mediator.Send(request);
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost("change-password")]
        public async Task<ActionResult<OperationResultResource>> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            var currentUserName = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (currentUserName == null)
            {
                return Unauthorized(new
                {
                    StatusCode = 401,
                    Message = "Unauthorized Access",
                    Details = "Cannot read username from token."
                });
            }

            var user = await unitOfWork.UserService.GetUserInfoAsyncByUsername(currentUserName);

            if (user.UserGuid != request.UserId)
            {

                return Unauthorized(new
                {
                    StatusCode = 401,
                    Message = "Unauthorized Access",
                    Details = "You can only change password for you own account."
                });
            }

            return await Mediator.Send(request);
        }
    }
}
