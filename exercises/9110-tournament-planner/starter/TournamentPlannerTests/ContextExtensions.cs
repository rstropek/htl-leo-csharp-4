using System;
using System.Threading.Tasks;
using TournamentPlanner.Data;

namespace TournamentPlannerTests
{
    /// <summary>
    /// Extensions methods for <see cref="TournamentPlannerDbContext"/>
    /// </summary>
    internal static class ContextExtensions
    {
        /// <summary>
        /// Add a given amount of dummy players
        /// </summary>
        public static async Task AddDummyPlayers(this TournamentPlannerDbContext context, int amount)
        {
            for (var i = 0; i < amount; i++)
            {
                await context.AddPlayer(new() { Name = $"Player {i + 1}" });
            }

            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Sets a random winner for all incomplete matches
        /// </summary>
        public static async Task SetWinnerForAllIncompleteMatches(this TournamentPlannerDbContext context)
        {
            var rand = new Random();
            foreach (var match in await context.GetIncompleteMatches())
            {
                await context.SetWinner(match.ID, rand.Next(2) == 0 ? PlayerNumber.Player1 : PlayerNumber.Player2);
            }

            await context.SaveChangesAsync();
        }
    }
}
