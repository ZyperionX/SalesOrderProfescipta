using System.ComponentModel.DataAnnotations;

namespace SalesOrderProfescipta.Server.Models
{
    // order have many item and one customer
    public class Order
    {
        public string Code { get; set; } = string.Empty;

        public DateTime ReleaseDate { get; set; }

        public int CustomerId { get; set; }
    }
}
