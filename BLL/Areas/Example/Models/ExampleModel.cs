using NetCoreApiBoilerplate.BLL.Areas.Common.Models;
using System.ComponentModel.DataAnnotations;

namespace NetCoreApiBoilerplate.BLL.Areas.Example.Models
{
    public class ExampleModel : BaseModel
    {
        [Key]
        public int ExampleId { get; set; }
        public string ExampleData { get; set; }
    }
}
