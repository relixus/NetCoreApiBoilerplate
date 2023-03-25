using Microsoft.AspNetCore.Identity;
using System.Diagnostics.CodeAnalysis;

namespace NetCoreApiBoilerplate.Areas.Auth.Models
{
    public class ApiUser : IdentityUser
    {
        [AllowNull]
        public DateTime? BlockedUntil { get; set; }
    }
}
