using Microsoft.AspNetCore.Identity;
using NetCoreApiBoilerplate.Areas;
using NetCoreApiBoilerplate.BLL.Areas.Auth.Models;
using NetCoreApiBoilerplate.BLL.Areas.Common.Models;
using NetCoreApiBoilerplate.Context;
using System;

namespace NetCoreApiBoilerplate.BLL.Areas.Auth.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApiUser> userManager;
        private readonly ILogger logger;

        public UserService(ServiceParams serviceParams)
        {
            userManager = serviceParams.UserManager;
        }


        public async Task<UserInfoResource> GetUserInfoAsyncByGuid(string guid)
        {

            var resource = new UserInfoResource();

            var userData = await userManager.FindByIdAsync(guid);

            if (userData == null) return null!;

            resource.UserGuid = guid;
            resource.Username = userData.UserName;
            resource.NormalizedUserName = userData.NormalizedUserName;
            resource.Claims = await userManager.GetClaimsAsync(userData);

            return resource;
        }

        public async Task<UserInfoResource> GetUserInfoAsyncByUsername(string uname)
        {

            var resource = new UserInfoResource();

            var userData = await userManager.FindByNameAsync(uname);

            if (userData == null) return null!;

            resource.UserGuid = userData.Id;
            resource.Username = userData.UserName;
            resource.NormalizedUserName = userData.NormalizedUserName;
            resource.Claims = await userManager.GetClaimsAsync(userData);

            return resource;
        }

        public async Task<OperationResultResource> AdminResetPassword(string userGuid, string newPassword)
        {
            var userData = await userManager.FindByIdAsync(userGuid);
            var token = await userManager.GeneratePasswordResetTokenAsync(userData);

            var result = await userManager.ResetPasswordAsync(userData, token, newPassword);

            if (result.Succeeded)
            {
                return new OperationResultResource { Success = true };
            }
            else
            {
                LogError(result.Errors);
                return new OperationResultResource
                {
                    Success = false,
                    Message = "Error Changing Password, Contact IT",
                };
            }
        }

        public async Task<OperationResultResource> ChangePassword(string userGuid, string oldPassword, string newPassword)
        {
            var userData = await userManager.FindByIdAsync(userGuid);
            var isOldPasswordCorrect = await userManager.CheckPasswordAsync(userData, oldPassword);

            if (!isOldPasswordCorrect)
            {
                return new OperationResultResource
                {
                    Success = false,
                    Message = "Old Password is incorrect",
                    OperationName = "UserService.ChangePassword"
                };
            }
            else
            {
                var result = await userManager.ChangePasswordAsync(userData, oldPassword, newPassword);

                if (result.Succeeded)
                {
                    return new OperationResultResource { Success = true };
                }
                else
                {
                    LogError(result.Errors);

                    return new OperationResultResource
                    {
                        Success = false,
                        Message = "Error Changing Password, Contact IT",
                    };
                }
            }


        }

       

        #region private methods

        private void LogError(IEnumerable<IdentityError> errors) { 
            foreach(var error in errors)
            {
                
                logger.LogError($"Identity Error(Change Password): {error.Code} - {error.Description}");
            }
        }

        #endregion
    }
}
