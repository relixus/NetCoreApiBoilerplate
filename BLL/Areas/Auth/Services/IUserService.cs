using NetCoreApiBoilerplate.BLL.Areas.Auth.Models;
using NetCoreApiBoilerplate.BLL.Areas.Common.Models;

namespace NetCoreApiBoilerplate.BLL.Areas.Auth.Services
{
    public interface IUserService
    {
        Task<UserInfoResource> GetUserInfoAsyncByGuid(string guid);
        Task<UserInfoResource> GetUserInfoAsyncByUsername(string uname);
        // TO BE USED BY SYSTEM ADMIN
        Task<OperationResultResource> AdminResetPassword(string userGuid, string newPassword);
        //TO BE USED BY USER
        Task<OperationResultResource> ChangePassword(string userGuid, string oldPassword,string newPassword);     
        
    }
}