using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NetCoreApiBoilerplate.BLL.Areas.Auth.Models;
using System.Runtime.CompilerServices;

namespace NetCoreApiBoilerplate.Context.Extensions
{
    public static class UserContextExtension
    {
        public static void SetUserClaimsTemplateBuilderExtension(this ModelBuilder builder)
        {
            builder.Entity<UserClaimsTemplate>().HasIndex(c => c.Area);
        }
    }
}
