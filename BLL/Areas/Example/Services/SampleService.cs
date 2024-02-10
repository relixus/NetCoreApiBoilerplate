using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using NetCoreApiBoilerplate.Areas;
using NetCoreApiBoilerplate.BLL.Areas.Common.Services;
using NetCoreApiBoilerplate.BLL.Areas.Example.Models;
using NetCoreApiBoilerplate.Context;

namespace NetCoreApiBoilerplate.BLL.Areas.Example.Services
{
    public class SampleService : ISampleInterface
    {
        private readonly ApplicationContext _context;
        private readonly IConfiguration configuration;
        private readonly string applicationConnectionString;

        public SampleService(ServiceParams par)
        {
            _context = par.ApplicationContext;
            configuration = par.Configuration;
            applicationConnectionString = par.ApplicationConnectionString;
        }

        public void EFCoreSave()
        {
            var repo = new CommonRepository<ExampleModel>(_context);
            repo.Insert(new ExampleModel
            {
                ExampleData = "A"
            });
            _context.SaveChanges();
        }

        public void SQLClientSave()
        {
            using (var sqlCon = new SqlConnection(applicationConnectionString))
            {
                if (sqlCon != null)
                {
                    sqlCon.Open();

                    using (var com = new SqlCommand())
                    {
                        com.Connection = sqlCon;
                        com.CommandText = "INSERT INTO [SAMPLE](ExampleData,CreatedDate,UpdatedDate) VALUES ('B', GETDATE(), GETDATE())";
                        com.ExecuteNonQuery();
                    }
                    sqlCon.Close();
                }
            }
        }
    }
}
