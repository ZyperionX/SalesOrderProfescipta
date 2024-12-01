namespace SalesOrderProfescipta.Server.Models
{
    public class SalesOrderListRequest
    {
        public string Keyword { get; set; } = string.Empty;

        public DateTime OrderDate { get; set; }
    }
}
