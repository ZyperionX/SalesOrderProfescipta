using System.ComponentModel.DataAnnotations;

namespace SalesOrderProfescipta.Server.Models
{
    public class CreateSalesOrderRequest
    {
        [MaxLength(10)]
        public string Code { get; set; } = string.Empty;

        public DateTime ReleaseDate { get; set; }

        public int CustomerId { get; set; }

        public string Address { get; set; } = string.Empty;

        public required List<ItemRequest> Items { get; set; }
    }

    public class ItemRequest
    {
        public string Name { get; set; } = string.Empty;

        public int Quantity { get; set; }

        public int Price { get; set; }
    }
}
