using Microsoft.AspNetCore.Mvc;
using SalesOrderProfescipta.Server.Services;

namespace SalesOrderProfescipta.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DropdownController : ControllerBase
    {
        private readonly DropdownService dropdownService;

        public DropdownController(DropdownService service)
        {
            dropdownService = service;
        }

        // GET: api/<DropdownController>
        [HttpGet("customer")]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            var CustomerList = await dropdownService.GetCustomerDropdownListAsync(cancellationToken);
            return Ok(CustomerList);
        }
    }
}
