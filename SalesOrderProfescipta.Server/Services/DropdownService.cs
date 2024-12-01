using Microsoft.EntityFrameworkCore;
using SalesOrderProfescipta.Server.Models;

namespace SalesOrderProfescipta.Server.Services
{
    public class DropdownService
    {
        private SalesDbContext _context;

        public DropdownService(SalesDbContext context)
        {
            this._context = context;
        }

        public async Task<List<BaseDropdownResponse<int>>> GetCustomerDropdownListAsync(CancellationToken cancellationToken)
        {
            var result = await _context.Customers
                .AsNoTracking()
                .Select(Q => new BaseDropdownResponse<int>
                { 
                    Id = Q.Id,
                    Label = Q.Name
                }).ToListAsync(cancellationToken);

            return result;
        }

    }
}
