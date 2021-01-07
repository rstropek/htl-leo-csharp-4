using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TournamentPlanner.Data
{
    public enum PlayerNumber { Player1 = 1, Player2 = 2 };

    public class TournamentPlannerDbContext : DbContext
    {
        public TournamentPlannerDbContext(DbContextOptions<TournamentPlannerDbContext> options)
            : base(options)
        { }

        // This class is NOT COMPLETE.
        // Todo: Complete the class according to the requirements

        /// <summary>
        /// Adds a new player to the player table
        /// </summary>
        /// <param name="newPlayer">Player to add</param>
        /// <returns>Player after it has been added to the DB</returns>
        public Task<Player> AddPlayer(Player newPlayer)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Adds a match between two players
        /// </summary>
        /// <param name="player1Id">ID of player 1</param>
        /// <param name="player2Id">ID of player 2</param>
        /// <param name="round">Number of the round</param>
        /// <returns>Generated match after it has been added to the DB</returns>
        public Task<Match> AddMatch(int player1Id, int player2Id, int round)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Set winner of an existing game
        /// </summary>
        /// <param name="matchId">ID of the match to update</param>
        /// <param name="player">Player who has won the match</param>
        /// <returns>Match after it has been updated in the DB</returns>
        public Task<Match> SetWinner(int matchId, PlayerNumber player)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get a list of all matches that do not have a winner yet
        /// </summary>
        /// <returns>List of all found matches</returns>
        public Task<IList<Match>> GetIncompleteMatches()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Delete everything (matches, players)
        /// </summary>
        public Task DeleteEverything()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get a list of all players whose name contains <paramref name="playerFilter"/>
        /// </summary>
        /// <param name="playerFilter">Player filter. If null, all players must be returned</param>
        /// <returns>List of all found players</returns>
        public Task<IList<Player>> GetFilteredPlayers(string playerFilter = null)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Generate match records for the next round
        /// </summary>
        /// <exception cref="InvalidOperationException">Error while generating match records</exception>
        public Task GenerateMatchesForNextRound()
        {
            throw new NotImplementedException();
        }
    }
}
