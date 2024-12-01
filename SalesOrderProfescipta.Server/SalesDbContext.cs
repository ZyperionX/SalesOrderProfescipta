using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SalesOrderProfescipta.Server.Entities;

namespace SalesOrderProfescipta.Server
{
    public class SalesDbContext : DbContext
    {

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("DB");
        //}
        protected readonly IConfiguration Configuration;

        public SalesDbContext(IConfiguration configuration, DbContextOptions<SalesDbContext> options)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sql server with connection string from app settings
            options.UseSqlServer(Configuration.GetConnectionString("Db"));
        }

        public virtual required DbSet<Customer> Customers { get; set; }

        public virtual required DbSet<Order> Orders { get; set; }

        public virtual required DbSet<Item> Items { get; set; }
    }
}
