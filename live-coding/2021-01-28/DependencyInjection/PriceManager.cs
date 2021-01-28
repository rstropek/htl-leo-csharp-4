using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DependencyInjection
{
    public class PriceManager
    {
        private readonly ProductContext context;

        public PriceManager(ProductContext context)
        {
            this.context = context;
        }

        public void ChangePrices(decimal percPriceChange)
        {
            foreach (var p in context.Prices)
            {
                p.ProductPrice *= percPriceChange;
            }
        }
    }
}
