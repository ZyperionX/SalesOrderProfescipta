using System.ComponentModel.DataAnnotations;

namespace SalesOrderProfescipta.Server.Models
{
    public class UpdateSalesOrderRequest
    {
        public DateTime ReleaseDate { get; set; }

        public int CustomerId { get; set; }

        public string Address { get; set; } = string.Empty;

        public required List<Item> Items { get; set; }
    }
}
