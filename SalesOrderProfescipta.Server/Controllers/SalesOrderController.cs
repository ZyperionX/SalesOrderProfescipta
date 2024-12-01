using System.Reflection.Metadata;
using System.Threading;
using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesOrderProfescipta.Server.Models;
using SalesOrderProfescipta.Server.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SalesOrderProfescipta.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesOrderController : ControllerBase
    {
        private readonly SalesOrderService salesOrderService;

        public SalesOrderController(SalesOrderService service)
        {
            salesOrderService = service;
        }

        /// <summary>
        /// Get sales order list.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] SalesOrderListRequest request,CancellationToken cancellationToken)
        {
            var OrderList = await salesOrderService.GetSalesOrderListAsync(request, cancellationToken);

            return Ok(OrderList);
        }

        /// <summary>
        /// Get sales order and its items detail.
        /// </summary>
        /// <param name="code"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("{code}")]
        public async Task<IActionResult> Get(string code, CancellationToken cancellationToken)
        {
            var OrderDetail = await salesOrderService.GetSalesOrderAsync(code, cancellationToken);
            return Ok(OrderDetail);
        }

        /// <summary>
        /// Create order sales and its items.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateSalesOrderRequest request)
        {
            var result = await salesOrderService.CreateSalesOrderAsync(request);

            return Ok();
        }

        /// <summary>
        /// Update order sales and its items based on code.
        /// </summary>
        /// <param name="code"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("{code}")]
        public async Task<IActionResult> Put(string code, [FromBody] UpdateSalesOrderRequest request)
        {
            var result = await salesOrderService.UpdateSalesOrderAsync(code, request);

            return Ok();
        }

        /// <summary>
        /// Delete order sales and its items based on code.
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpDelete("{code}")]
        public async Task<IActionResult> Delete(string code)
        {
            var result = await salesOrderService.DeleteSalesOrderAsync(code);

            return Ok();
        }

        /// <summary>
        /// Generate order sales in excel.
        /// </summary>
        /// <returns></returns>
        [HttpGet("excel")]
        public async Task<IActionResult> GenerateExcel()
        {
            var result = await salesOrderService.GenerateExcelAsync();

            return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "SalesOrder.xlsx");
        }
    }
}
