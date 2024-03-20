using Billing_API_Net8.Models;
using Billing_API_NET8.Models;
using Microsoft.EntityFrameworkCore;

namespace Billing_API_NET8.Helpers
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
        : base(options)
        {
        }

        public DbSet<Client> Client { get; set; }
        //public DbSet<SystemUserRole> SystemUserRole { get; set; }

        public DbSet<Currency> Currency { get; set; }
        //public DbSet<SystemUserRole> SystemUserRole { get; set; }
        public DbSet<SystemUser> SystemUser { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<ClientAddress>()
            //.HasOne<Client>(s => s.Client)
            //.WithMany().OnDelete(DeleteBehavior.NoAction);
        }
        public DbSet<Invoice> Invoice{ get; set; }
        //public DbSet<SystemUserRole> SystemUserRole { get; set; }


        //protected override void OnConfiguring(DbContextOptionsBuilder options)
        //{
        //    // in memory database used for simplicity, change to a real db for production applications
        //    //options.UseInMemoryDatabase("TestDb");
        //}
    }
}