using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using NetCoreApiBoilerplate.BLL.Areas.Auth.Models;
using NetCoreApiBoilerplate.Context;

namespace NetCoreApiBoilerplate.Areas
{
    public class ServiceParams
    {
        public ApplicationContext ApplicationContext { get; set; }
        public IConfiguration Configuration { get; set; }
        public string ApplicationConnectionString { get; set; }
        public UserManager<ApiUser> UserManager { get; set; }
    }
}
