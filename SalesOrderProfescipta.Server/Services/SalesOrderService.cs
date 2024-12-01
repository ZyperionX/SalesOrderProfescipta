using System.Net;
using Azure.Core;
using System.Threading;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesOrderProfescipta.Server.Entities;
using SalesOrderProfescipta.Server.Models;
using DocumentFormat.OpenXml.Spreadsheet;

namespace SalesOrderProfescipta.Server.Services
{
    public class SalesOrderService
    {
        private SalesDbContext _context;

        public SalesOrderService(SalesDbContext context)
        {
            this._context = context;
        }

        public async Task<List<SalesOrderListResponse>> GetSalesOrderListAsync(SalesOrderListRequest request, CancellationToken cancellationToken)
        {
            var result = await (from o in _context.Orders
                                  join c in _context.Customers on o.CustomerId equals c.Id
                                  where (request.Keyword == string.Empty ? true : o.Code.Contains(request.Keyword))
                                    && (request.OrderDate.Year == 1 ? true : (o.ReleaseDate >= request.OrderDate && o.ReleaseDate < request.OrderDate.AddDays(1)))
                                  select new SalesOrderListResponse
                                  {
                                      Code = o.Code,
                                      ReleaseDate = o.ReleaseDate,
                                      CustomerName = c.Name,
                                      Address = o.Address,
                                  })
                                  .AsNoTracking()
                                  .OrderBy(Q => Q.ReleaseDate)
                                  .ToListAsync(cancellationToken);

            return result;
        }

        public async Task<List<SalesOrderResponse>> GetSalesOrderAsync(string code, CancellationToken cancellationToken)
        {
            var result = await (from o in _context.Orders
                                where code == o.Code
                                select new SalesOrderResponse
                                {
                                    Code = o.Code,
                                    ReleaseDate = o.ReleaseDate,
                                    CustomerId = o.CustomerId,
                                    Address = o.Address,
                                    Items = (from i in _context.Items
                                             where (code == i.OrderCode)
                                             select new Models.Item
                                             {
                                                 Id = i.Id,
                                                 Quantity = i.Quantity,
                                                 Price = i.Price,
                                             }).ToList()
                                })
                                .AsNoTracking()
                                .ToListAsync(cancellationToken);

            return result;
        }

        public async Task<bool> CreateSalesOrderAsync(CreateSalesOrderRequest request)
        {
            var order = new Order { 
                Code = request.Code,
                ReleaseDate = request.ReleaseDate,
                CustomerId = request.CustomerId,
                Address = request.Address,
            };

            _context.Orders.Add(order);

            foreach (var item in request.Items)
            {
                _context.Items.Add(new Entities.Item
                {
                    Name = item.Name,
                    Price = item.Price,
                    Quantity = item.Quantity,
                    OrderCode = request.Code
                });
            }
            
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateSalesOrderAsync(string code, UpdateSalesOrderRequest request)
        {
            var order = await _context.Orders.Where(Q => Q.Code == code).FirstOrDefaultAsync();

            if (order == null)
            {
                return false;
            }

            order.ReleaseDate = request.ReleaseDate;
            order.CustomerId = request.CustomerId;
            order.Address = request.Address;

            _context.Orders.Update(order);

            var items = await _context.Items.Where(Q => Q.OrderCode == code).ToListAsync();

            if (items.Count == 0)
            {
                return false;
            }

            var updatedItems = new List<Entities.Item>();

            foreach (var item in request.Items)
            {
                // If item id is 0, the item is new.
                if (item.Id == 0)
                {
                    _context.Items.Add(new Entities.Item
                    {
                        Name = item.Name,
                        Price = item.Price,
                        Quantity = item.Quantity,
                        OrderCode = code
                    });

                    continue;
                }

                // Search for item with same id.
                var tempItem = items.Where(Q => Q.Id == item.Id).FirstOrDefault();

                if (tempItem == null)
                {
                    continue;
                }

                // If found, remove the item from list to improve efficiency.
                items.Remove(tempItem);

                tempItem.Price = item.Price;
                tempItem.Quantity = item.Quantity;
                tempItem.Name = item.Name;
                tempItem.OrderCode = code;

                updatedItems.Add(tempItem);
            }

            // If the item id not found, it have been deleted.
            _context.Items.RemoveRange(items);

            _context.Items.UpdateRange(updatedItems);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteSalesOrderAsync(string code)
        {

            var tempItems = await _context.Items.Where(Q => Q.OrderCode == code).ToListAsync();
            var tempOrder = await _context.Orders.Where(Q => Q.Code == code).FirstOrDefaultAsync();

            if(tempOrder == null ||  tempItems.Count == 0)
            {
                return false;
            }

            _context.Items.RemoveRange(tempItems);
            _context.Orders.Remove(tempOrder);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<byte[]> GenerateExcelAsync()
        {
            var result = await(from o in _context.Orders
                               join c in _context.Customers on o.CustomerId equals c.Id
                               select new SalesOrderListResponse
                               {
                                   Code = o.Code,
                                   ReleaseDate = o.ReleaseDate,
                                   CustomerName = c.Name,
                                   Address = o.Address,
                               })
                                  .AsNoTracking()
                                  .OrderBy(Q => Q.ReleaseDate)
                                  .ToListAsync();

            if(result.Count == 0)
            {
                return Array.Empty<byte>();
            }

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Sales Order");
                workbook.Author = "Evan Marvel";

                worksheet.Cell(1, 1).Value = "No";
                worksheet.Cell(1, 2).Value = "Sales Order";
                worksheet.Cell(1, 3).Value = "Order Date";
                worksheet.Cell(1, 4).Value = "Customer";

                worksheet.Cell(1, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                worksheet.Cell(1, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                worksheet.Cell(1, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                worksheet.Cell(1, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                worksheet.Cell(1, 1).Style.Font.Bold = true;
                worksheet.Cell(1, 2).Style.Font.Bold = true;
                worksheet.Cell(1, 3).Style.Font.Bold = true;
                worksheet.Cell(1, 4).Style.Font.Bold = true;

                worksheet.Cell(1, 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(1, 2).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(1, 3).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(1, 4).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                var i = 2;

                foreach (var order in result)
                {
                    worksheet.Cell(i, 1).Value = i-1;
                    worksheet.Cell(i, 2).Value = order.Code;
                    worksheet.Cell(i, 3).Value = order.ReleaseDate;
                    worksheet.Cell(i, 4).Value = order.CustomerName;

                    worksheet.Cell(i, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                    i++;
                }

                worksheet.Column(1).AdjustToContents();
                worksheet.Column(2).AdjustToContents();
                worksheet.Column(3).AdjustToContents();
                worksheet.Column(4).AdjustToContents();

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    return stream.ToArray();
                }
            }
        }
    }
}
