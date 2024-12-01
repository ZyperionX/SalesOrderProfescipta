using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace SalesOrderProfescipta.Server.Entities
{
    public class Item
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(10)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string OrderCode { get; set; } = string.Empty;
        public Order Order { get; set; }

        public string Name { get; set; } = string.Empty;

        public int Quantity { get; set; }

        public int Price { get; set; }
    }
}
