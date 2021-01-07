using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TournamentPlanner.Data;

namespace TournamentPlanner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly TournamentPlannerDbContext context;

        public PlayersController(TournamentPlannerDbContext context)
            => this.context = context;

        [HttpPost]
        public async Task<Player> AddPlayer([FromBody] Player newPlayer)
        {
            await context.AddPlayer(newPlayer);
            return newPlayer;
        }

        [HttpGet]
        public async Task<IEnumerable<Player>> GetPlayers([FromQuery] string name = null)
            => await context.GetFilteredPlayers(name);
    }
}
