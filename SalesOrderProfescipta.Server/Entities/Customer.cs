using System.ComponentModel.DataAnnotations;

namespace SalesOrderProfescipta.Server.Entities
{
    public class Customer
    {
        [Required]
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public virtual ICollection<Order> Orders { get; set; }
    }
}
