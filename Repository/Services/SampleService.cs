using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using NetCoreApiBoilerplate.Context;
using NetCoreApiBoilerplate.Context.Models;
using NetCoreApiBoilerplate.Repository.Abstract;

namespace NetCoreApiBoilerplate.Repository.Services
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
            var entity = new ExampleModel() { ExampleData = "A" };
            _context.Sample.Add(entity);
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
