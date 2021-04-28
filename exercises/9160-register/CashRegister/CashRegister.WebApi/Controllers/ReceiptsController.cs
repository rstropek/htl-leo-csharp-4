using CashRegister.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CashRegister.WebApi.Controllers
{
    [ApiController]
    [Route("api/receipts")]
    public class ReceiptsController : ControllerBase
    {
        private readonly CashRegisterDataContext DataContext;

        public ReceiptsController(CashRegisterDataContext dataContext)
        {
            DataContext = dataContext;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] List<ReceiptLineDto> receiptLineDto)
        {
            if (receiptLineDto == null || receiptLineDto.Count == 0)
            {
                return BadRequest("Missing receipt lines");
            }

            // Read product data from DB for incoming product IDs
            var products = new Dictionary<int, Product>();
            foreach (var rl in receiptLineDto)
            {
                products[rl.ProductID] = await DataContext.Products.FirstOrDefaultAsync(p => p.ID == rl.ProductID);
                if (products[rl.ProductID] == null)
                {
                    return BadRequest($"Unknown product ID {rl.ProductID}");
                }
            }

            // Build receipt from DTO
            var newReceipt = new Receipt
            {
                ReceiptTimestamp = DateTime.UtcNow,
                ReceiptLines = receiptLineDto.Select(rl => new ReceiptLine
                {
                    ID = 0,
                    Product = products[rl.ProductID],
                    Amount = rl.Amount,
                    TotalPrice = rl.Amount * products[rl.ProductID].UnitPrice
                }).ToList()
            };
            newReceipt.TotalPrice = newReceipt.ReceiptLines.Sum(rl => rl.TotalPrice);

            await DataContext.Receipts.AddAsync(newReceipt);
            await DataContext.SaveChangesAsync();

            return StatusCode((int)HttpStatusCode.Created, newReceipt);
        }
    }
}
