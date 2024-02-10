using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace NetCoreApiBoilerplate.BLL.Areas.Auth.Models
{
    public class UserInfoResource
    {
        public string UserGuid { get; set; }
        public string Username { get; set; }
        public string NormalizedUserName { get; set; }
        public IEnumerable<Claim> Claims { get; set; }

    }
}
