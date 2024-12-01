using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesOrderProfescipta.Server.Entities
{
    // order have many item and one customer
    public class Order
    {
        [Key, MaxLength(10)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Code { get; set; } = string.Empty;
        public virtual ICollection<Item> items { get; set; }

        public DateTime ReleaseDate { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public string Address { get; set; } = string.Empty;
    }
}
