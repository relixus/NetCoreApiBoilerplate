using NetCoreApiBoilerplate.Context;
using System.Transactions;
using NetCoreApiBoilerplate.BLL.Areas.Example.Services;
using NetCoreApiBoilerplate.BLL.Areas.Auth.Services;
using Microsoft.AspNetCore.Identity;
using NetCoreApiBoilerplate.BLL.Areas.Auth.Models;

namespace NetCoreApiBoilerplate.Areas
{
    public class UnitOfWork
    {
        private readonly ApplicationContext _context;
        private TransactionScope transactionScope;


        public ISampleInterface Sample { get; set; }
        public IUserService UserService { get; set; }


        public UnitOfWork(ApplicationContext context, 
                            IConfiguration configuration, 
                            UserManager<ApiUser> userManager)
        {
            _context = context;

            var pars = new ServiceParams()
            {
                Configuration = configuration,
                ApplicationContext = _context,
                ApplicationConnectionString = configuration.GetConnectionString("App") ?? throw new InvalidOperationException(),
                UserManager = userManager
            };

            Sample = new SampleService(pars);
            UserService = new UserService(pars);
        }

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
