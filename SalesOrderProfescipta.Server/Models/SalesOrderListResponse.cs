using SalesOrderProfescipta.Server.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SalesOrderProfescipta.Server.Models
{
    public class SalesOrderListResponse
    {
        [MaxLength(10)]
        public string Code { get; set; } = string.Empty;

        public DateTime ReleaseDate { get; set; }

        public string CustomerName { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;
    }
}
