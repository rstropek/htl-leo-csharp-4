using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TournamentPlanner.Data;

namespace TournamentPlanner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchesController : ControllerBase
    {
        private readonly TournamentPlannerDbContext context;

        public MatchesController(TournamentPlannerDbContext context) => this.context = context;

        [HttpGet]
        [Route("open")]
        public async Task<IEnumerable<Match>> GetIncompleteMatches() => await context.GetIncompleteMatches();

        [HttpPost]
        [Route("generate")]
        public async Task GenerateRound()
            => await context.GenerateMatchesForNextRound();
    }
}
