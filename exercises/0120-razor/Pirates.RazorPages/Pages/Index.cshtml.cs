using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Pirates.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pirates.RazorPages.Pages
{
    public class IndexModel : PageModel
    {
        private readonly PiratesContext context;

        public IEnumerable<Pirate>? Pirates { get; private set; }

        [BindProperty]
        public string? NameFilter { get; set; }

        public IndexModel(PiratesContext context)
        {
            this.context = context;
        }

        public async Task OnGetAsync() => await OnPostFilter();

        public async Task OnPostDelete(Guid id)
        {
            var pirate = await context.Pirates.FirstOrDefaultAsync(p => p.ID == id);
            if (pirate != null)
            {
                context.Pirates.Remove(pirate);
                await context.SaveChangesAsync();
            }

            await OnPostFilter();
        }

        public async Task OnPostFilter()
        {
            IQueryable<Pirate> result = context.Pirates;
            if (!string.IsNullOrEmpty(NameFilter))
            {
                result = result.Where(p => (p.Name != null && p.Name.Contains(NameFilter)) ||
                    (p.RealName != null && p.RealName.Contains(NameFilter)));
            }

            Pirates = await result.ToListAsync();
        }
    }
}
