using CashRegister.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CashRegister.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly CashRegisterDataContext DataContext;

        public ProductsController(CashRegisterDataContext dataContext)
        {
            DataContext = dataContext;
        }

        [HttpGet]
        public async Task<IEnumerable<Product>> Get([FromQuery] string? nameFilter = null)
        {
            IQueryable<Product> products = DataContext.Products;

            // Apply filter if one is given
            if (!string.IsNullOrEmpty(nameFilter))
            {
                products = products.Where(p => p.ProductName.Contains(nameFilter));
            }

            return await products.ToListAsync();
        }
    }
}
