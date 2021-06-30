using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Pirates.Model;

namespace Pirates.RazorPages.Pages
{
    public class PirateModel : PageModel
    {
        private readonly PiratesContext context;

        public PirateModel(PiratesContext context)
        {
            this.context = context;
            Pirate = new Pirate();
        }

        [BindProperty]
        public Pirate Pirate { get; set; }

        public async Task OnGetAsync()
        {
            var idString = RouteData.Values["id"] as string;
            if (idString != null && Guid.TryParse(idString, out var id))
            {
                var dbPirate = await context.Pirates.FirstOrDefaultAsync(p => p.ID == id);
                if (dbPirate != null) Pirate = dbPirate;
            }
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid && Pirate != null)
            {
                if (RouteData.Values["id"] != null)
                {
                    context.Pirates.Update(Pirate);
                }
                else
                {
                    context.Pirates.Add(Pirate);
                }

                await context.SaveChangesAsync();
                return RedirectToPage("Index");
            }

            return Page();
        }
    }
}
