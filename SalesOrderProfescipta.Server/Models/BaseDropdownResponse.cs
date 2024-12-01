namespace SalesOrderProfescipta.Server.Models
{
    public class BaseDropdownResponse<T>
    {
        public required T Id { get; set; }

        public required string Label { get; set; }
    }
}
