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

        public DbSet<Player> Players { get; set; }

        public DbSet<Match> Matches { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Match>()
                .HasOne(m => m.Player1)
                .WithMany()
                .HasForeignKey(m => m.Player1ID)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Match>()
                .HasOne(m => m.Player2)
                .WithMany()
                .HasForeignKey(m => m.Player2ID)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Match>()
                .HasOne(m => m.Winner)
                .WithMany()
                .HasForeignKey(m => m.WinnerID)
                .OnDelete(DeleteBehavior.NoAction);
        }

        /// <summary>
        /// Adds a new player to the player table
        /// </summary>
        /// <param name="newPlayer">Player to add</param>
        /// <returns>Player after it has been added to the DB</returns>
        public async Task<Player> AddPlayer(Player newPlayer)
        {
            Players.Add(newPlayer);
            await SaveChangesAsync();
            return newPlayer;
        }

        /// <summary>
        /// Adds a match between two players
        /// </summary>
        /// <param name="player1Id">ID of player 1</param>
        /// <param name="player2Id">ID of player 2</param>
        /// <param name="round">Number of the round</param>
        /// <returns>Generated match after it has been added to the DB</returns>
        public async Task<Match> AddMatch(int player1Id, int player2Id, int round)
        {
            var match = new Match
            {
                Player1ID = player1Id,
                Player2ID = player2Id,
                Round = round
            };
            Matches.Add(match);
            await SaveChangesAsync();
            return match;
        }

        /// <summary>
        /// Set winner of an existing game
        /// </summary>
        /// <param name="matchId">ID of the match to update</param>
        /// <param name="player">Player who has won the match</param>
        /// <returns>Match after it has been updated in the DB</returns>
        public async Task<Match> SetWinner(int matchId, PlayerNumber player)
        {
            var match = Matches.Single(m => m.ID == matchId);
            match.WinnerID = player switch {
                PlayerNumber.Player1 => match.Player1ID,
                PlayerNumber.Player2 => match.Player2ID,
                _ => throw new ArgumentOutOfRangeException(nameof(player))
            };
            await SaveChangesAsync();
            return match;
        }

        /// <summary>
        /// Get a list of all matches that do not have a winner yet
        /// </summary>
        /// <returns>List of all found matches</returns>
        public async Task<IList<Match>> GetIncompleteMatches()
            => await Matches.Where(m => m.Winner == null).ToListAsync();

        /// <summary>
        /// Delete everything (matches, players)
        /// </summary>
        public async Task DeleteEverything()
        {
            using var transaction = await Database.BeginTransactionAsync();
            await Database.ExecuteSqlRawAsync("DELETE FROM dbo.Matches");
            await Database.ExecuteSqlRawAsync("DELETE FROM dbo.Players");
            await transaction.CommitAsync();
        }

        /// <summary>
        /// Get a list of all players whose name contains <paramref name="playerFilter"/>
        /// </summary>
        /// <param name="playerFilter">Player filter. If null, all players must be returned</param>
        /// <returns>List of all found players</returns>
        public async Task<IList<Player>> GetFilteredPlayers(string playerFilter = null) =>
            await Players.Where(p => playerFilter == null || p.Name.Contains(playerFilter)).ToListAsync();

        /// <summary>
        /// Generate match records for the next round
        /// </summary>
        /// <exception cref="InvalidOperationException">Error while generating match records</exception>
        public async Task GenerateMatchesForNextRound()
        {
            using var transaction = await Database.BeginTransactionAsync();

            // If there are *any* matches in the DB that do *not* have a winner, throw an exception.
            if ((await GetIncompleteMatches()).Any()) throw new InvalidOperationException("Incomplete matches");

            // If the number of players in the DB is not 32, throw an exception.
            var players = await GetFilteredPlayers();
            if (players.Count != 32) throw new InvalidOperationException("Incorrect number of players");

            var numberOfMatches = await Matches.CountAsync();
            switch (numberOfMatches)
            {
                case 0:
                    AddFirstRound(Matches, players);
                    break;
                case var n when n is 16 or 24 or 28 or 30:
                    await AddSubsequentRound(Matches);
                    break;
                default:
                    // If there is any other number of matches in the DB, throw an exception.
                    throw new InvalidOperationException("Invalid number of rounds");
            }

            await SaveChangesAsync();
            await transaction.CommitAsync();

            static void AddFirstRound(DbSet<Match> matches, IList<Player> players)
            {
                var rand = new Random();
                // If you find no matches in the DB, the next round is the first round.
                for (var i = 0; i < 16; i++)
                {
                    // Add a match with two random players
                    var player1 = players[rand.Next(players.Count)];
                    players.Remove(player1);
                    var player2 = players[rand.Next(players.Count)];
                    players.Remove(player2);
                    matches.Add(new Match
                    {
                        Player1 = player1,
                        Player2 = player2,
                        Round = 1
                    });
                }
            }

            static async Task AddSubsequentRound(DbSet<Match> matches)
            {
                var rand = new Random();

                // Get matches from last completed round
                var prevRound = await matches.MaxAsync(m => m.Round);
                var prevRoundMatches = await matches.Where(m => m.Round == prevRound).ToListAsync();
                var nextRound = prevRound + 1;
                for (var i = prevRoundMatches.Count / 2; i > 0; i--)
                {
                    // Add a match with two random winners from prev. round
                    var match1 = prevRoundMatches[rand.Next(prevRoundMatches.Count)];
                    prevRoundMatches.Remove(match1);
                    var match2 = prevRoundMatches[rand.Next(prevRoundMatches.Count)];
                    prevRoundMatches.Remove(match2);
                    matches.Add(new Match
                    {
                        Player1 = match1.Winner,
                        Player2 = match2.Winner,
                        Round = nextRound
                    });
                }
            }
        }
    }
}
