using Microsoft.EntityFrameworkCore;
using NetCoreApiBoilerplate.BLL.Areas.Common.Models;
using NetCoreApiBoilerplate.BLL.Areas.Example.Models;
using NetCoreApiBoilerplate.Context.Extensions;

namespace NetCoreApiBoilerplate.Context
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        public DbSet<ExampleModel> Sample { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.SetExampleModelBuilderExtension();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            SetUpBaseValues();
            return await base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges()
        {
            SetUpBaseValues();
            return base.SaveChanges();
        }

        private void SetUpBaseValues()
        {
            var entries = ChangeTracker
               .Entries()
               .Where(e => e.Entity is BaseModel && (
                       e.State == EntityState.Added
                       || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((BaseModel)entityEntry.Entity).UpdatedDate = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseModel)entityEntry.Entity).CreatedDate = DateTime.Now;
                }
            }
        }
    }
}
