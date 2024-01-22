using Microsoft.EntityFrameworkCore.Diagnostics;
using NetCoreApiBoilerplate.Context;
using NetCoreApiBoilerplate.Repository.Abstract;
using NetCoreApiBoilerplate.Repository.Services;
using System.Transactions;

namespace NetCoreApiBoilerplate.Repository
{
    public class ApplicationRepository
    {
        private readonly ApplicationContext _context;
        private TransactionScope transactionScope;
        public ApplicationRepository(ApplicationContext context, IConfiguration configuration)
        {
            _context = context;

            var pars = new ServiceParams()
            {
                Configuration = configuration,
                ApplicationContext = _context,
                ApplicationConnectionString = configuration.GetConnectionString("App") ?? throw new InvalidOperationException(),
            };

            Sample = new SampleService(pars);
        }

        public ISampleInterface Sample { get; set; }


        public void BeginTransaction()
        {
            transactionScope = new TransactionScope(TransactionScopeOption.Required, 
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted });
        }

        public void SaveChanges()
        {
            transactionScope.Complete();
            transactionScope.Dispose();
        }

        public void Rollback() {
            transactionScope.Dispose();
        }
    }
}
