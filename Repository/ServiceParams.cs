using Microsoft.Data.SqlClient;
using NetCoreApiBoilerplate.Context;

namespace NetCoreApiBoilerplate.Repository
{
    public class ServiceParams
    {
        public ApplicationContext ApplicationContext { get; set; }
        public IConfiguration Configuration { get; set; }
        public string ApplicationConnectionString { get; set; }
    }
}
