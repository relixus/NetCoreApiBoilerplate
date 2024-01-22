using System.ComponentModel.DataAnnotations;

namespace NetCoreApiBoilerplate.Context.Models
{
    public class ExampleModel : BaseModel
    {
        [Key]
        public int ExampleId { get; set; }
        public string ExampleData { get; set; }
    }
}
