using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Quotexchange.Api.Data;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Quotexchange.Api.Controllers
{
    // DTO class
    // Data annotations are important!
    public class NewQuote
    {
        [MinLength(5)]
        [MaxLength(500)]
        public string Quote { get; set; } = string.Empty;

        [MinLength(1)]
        [MaxLength(150)]
        public string Source { get; set; } = string.Empty;
    }

    [Route("api/[controller]")]
    [ApiController]
    public class QuotesController : ControllerBase
    {
        private readonly QuotexchangeDataContext dataContext;

        public QuotesController(QuotexchangeDataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        private async Task<User?> GetCurrentUserFromDb()
        {
            // Get name identifier from token
            var currentUserNameIdentifier = User.Claims.First(u => u.Type == ClaimTypes.NameIdentifier).Value;

            // Get user from DB
            return await dataContext.Users.FirstOrDefaultAsync(u => u.NameIdentifier == currentUserNameIdentifier);
        }

        [HttpPost("clear")]
        [Authorize] // <<< Important!
        public async Task<ActionResult> Clear()
        {
            foreach (var vote in await dataContext.Votes.ToListAsync()) dataContext.Votes.Remove(vote);
            foreach (var quote in await dataContext.Quotes.ToListAsync()) dataContext.Quotes.Remove(quote);
            // Must not delete users

            await dataContext.SaveChangesAsync();

            return NoContent(); // <<< Important!
        }

        [HttpGet("my")]
        [Authorize] // <<< Important!
        public async Task<ActionResult> GetMyQuotes()
        {
            // Get current user from DB, check if exists
            var currentUser = await GetCurrentUserFromDb();
            if (currentUser == null) return Unauthorized();

            // Return filtered list of quotes.
            return Ok(await dataContext.Quotes
                .Where(q => q.CreatorId == currentUser.Id)
                .Select(q => new { q.Id, Creator = q.Creator!.NameIdentifier, q.Quote, q.Source })
                .ToListAsync());
        }

        [HttpPost]
        [Authorize] // <<< Important!
        public async Task<ActionResult> AddQuote([FromBody] NewQuote q)
        {
            // Get current user from DB, check if exists
            var currentUser = await GetCurrentUserFromDb();
            if (currentUser == null) return Unauthorized();

            // Add new quote
            var newQuote = new FunnyQuote()
            {
                Creator = currentUser,
                Quote = q.Quote,
                Source = q.Source
            };
            dataContext.Quotes.Add(newQuote);
            await dataContext.SaveChangesAsync();
            return Ok(new
            {
                newQuote.Id,
                Creator = currentUser.NameIdentifier,
                newQuote.Quote,
                newQuote.Source
            });
        }

        [HttpGet] // <<< Important that no Authorize attribute is present
        public async Task<ActionResult> GetQuotes()
            => Ok(await dataContext.Quotes
                .Select(q => new { q.Id, Creator = q.Creator!.NameIdentifier, q.Quote, q.Source, Vote = q.Votes!.Sum(v => (int)v.UpDown) })
                .OrderByDescending(q => q.Vote)
                .Select(q => new { q.Id, q.Creator, q.Quote, q.Source })
                .ToListAsync());

        [HttpPost("{id}/vote")]
        [Authorize] // <<< Important!
        public async Task<ActionResult> Vote(int id, [FromQuery(Name = "v")] UpDown upDown)
        {
            // Get current user from DB, check if exists
            var currentUser = await GetCurrentUserFromDb();
            if (currentUser == null) return Unauthorized();

            // Check if quote exists
            var quote = await dataContext.Quotes.FirstOrDefaultAsync(q => q.Id == id);
            if (quote == null) return NotFound();

            // Add vote to DB
            var newVote = new Vote()
            {
                Quote = quote,
                UpDown = upDown
            };
            dataContext.Votes.Add(newVote);
            await dataContext.SaveChangesAsync();
            return Ok();
        }
    }
}
