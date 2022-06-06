using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.DB
{
    public class StoreContext: DbContext
    {
        public StoreContext() { }
        public StoreContext( DbContextOptions<StoreContext> options):base(options) { }
        public DbSet<Customer> customers { get; set; }
        public DbSet<Addresses> addresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }

    }
}
