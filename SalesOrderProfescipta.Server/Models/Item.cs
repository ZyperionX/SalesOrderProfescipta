using System.ComponentModel.DataAnnotations;

namespace SalesOrderProfescipta.Server.Models
{
    public class Item
    {
        public int Id { get; set; }

        public string SalesOrderCode { get; set; } = string.Empty;

        public int Quantity { get; set; }

        public int Price { get; set; }

        public virtual required ICollection<Order> Orders { get; set; }
    }
}
