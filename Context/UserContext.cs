using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NetCoreApiBoilerplate.Areas.Auth.Models;

namespace NetCoreApiBoilerplate.Context
{
    public class UserContext :IdentityUserContext<ApiUser>
    {
        public UserContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
