using System.ComponentModel.DataAnnotations;

namespace SalesOrderProfescipta.Server.Models
{
    public class Customer
    {
        [Required]
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public virtual required ICollection<Order> Orders { get; set; }
    }
}
