using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NetCoreApiBoilerplate.BLL.Areas.Auth.Models
{
    public class UserClaimsTemplate
    {
        [Key]
        public int UserClaimsTemplateId { get; set; }
        [MaxLength(200)]
        public string Area { get; set; }
        [MaxLength(200)]
        public string ClaimName { get; set; }
        public string Description { get; set; }
        [DefaultValue(true)]
        public bool Active { get; set; }
    }
}