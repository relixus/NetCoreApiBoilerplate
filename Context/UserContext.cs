using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NetCoreApiBoilerplate.BLL.Areas.Auth.Models;
using NetCoreApiBoilerplate.Context.Extensions;

namespace NetCoreApiBoilerplate.Context
{
    public class UserContext :IdentityUserContext<ApiUser>
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options) { }

        DbSet<UserClaimsTemplate> ClaimsTemplate { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.SetUserClaimsTemplateBuilderExtension();
        }
    }
}
