using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pirates.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pirates.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PiratesController : ControllerBase
    {
        private readonly PiratesContext context;

        public PiratesController(PiratesContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pirate>>> GetPirateByName([FromQuery(Name = "q")] string? query)
        {
            IQueryable<Pirate> result = context.Pirates;
            if (!string.IsNullOrEmpty(query))
            {
                result = result.Where(p => (p.Name != null && p.Name.Contains(query)) ||
                    (p.RealName != null && p.RealName.Contains(query)));
            }

            return Ok(await result.ToListAsync());
        }

        [HttpGet("{id}", Name = nameof(GetPirateById))]
        public async Task<ActionResult<Pirate>> GetPirateById(Guid id)
        {
            var pirate = await context.Pirates.FirstOrDefaultAsync(p => p.ID == id);
            if (pirate == null) return NotFound();
            return Ok(pirate);
        }

        [HttpPost]
        public async Task<ActionResult<Pirate>> AddPirate([FromBody] NewPirate pirate)
        {
            var p = new Pirate
            {
                Name = pirate.Name,
                RealName = pirate.RealName,
                YearOfBirth = pirate.YearOfBirth,
                YearOfDeath = pirate.YearOfDeath,
                CountryOfOrigin = pirate.CountryOfOrigin
            };
            await context.Pirates.AddAsync(p);
            await context.SaveChangesAsync();
            return CreatedAtRoute(nameof(GetPirateById), new { id = p.ID }, p);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePirateById(Guid id)
        {
            var pirate = await context.Pirates.FirstOrDefaultAsync(p => p.ID == id);
            if (pirate == null) return NotFound();

            context.Remove(pirate);
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
