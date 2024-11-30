using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SalesOrderProfescipta.Server.Models;

namespace SalesOrderProfescipta.Server
{
    public class SalesDbContext(DbContextOptions<SalesDbContext> options) : DbContext(options)
    {

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("DB");
        //}

        public virtual required DbSet<Customer> Customers { get; set; }

        public virtual required DbSet<Order> Orders { get; set; }

        public virtual required DbSet<Item> Items { get; set; }
    }
}
