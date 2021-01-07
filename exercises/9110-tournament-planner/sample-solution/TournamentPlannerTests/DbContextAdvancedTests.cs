using System;
using System.Threading.Tasks;
using Xunit;

namespace TournamentPlannerTests
{
    /// <summary>
    /// Test cases for advanced requirements
    /// </summary>
    public class DbContextAdvancedTests : IClassFixture<DbContextFactory>
    {
        private readonly DbContextFactory factory;

        public DbContextAdvancedTests(DbContextFactory factory) => this.factory = factory;

        [Fact]
        public async Task GetFilteredPlayersAll()
        {
            using var context = factory.GetContext();
            await context.DeleteEverything();
            await context.AddPlayer(new() { Name = "FooBar" });
            await context.SaveChangesAsync();
            Assert.Single(await context.GetFilteredPlayers());
        }

        [Fact]
        public async Task GetFilteredPlayers()
        {
            using var context = factory.GetContext();
            await context.DeleteEverything();
            await context.AddPlayer(new() { Name = "FooBar" });
            await context.SaveChangesAsync();
            Assert.Empty(await context.GetFilteredPlayers("Baz"));
        }

        [Fact]
        public async Task GenerateMatchWithIncomplete()
        {
            using var context = factory.GetContext();
            await context.DeleteEverything();
            var foo = await context.AddPlayer(new() { Name = "Foo" });
            var bar = await context.AddPlayer(new() { Name = "Bar" });
            var m = await context.AddMatch(foo.ID, bar.ID, 1);
            await Assert.ThrowsAsync<InvalidOperationException>(() => context.GenerateMatchesForNextRound());
        }

        [Fact]
        public async Task GenerateMatchWithInvalidPlayerCount()
        {
            using var context = factory.GetContext();
            await context.DeleteEverything();
            await context.AddDummyPlayers(31);
            await Assert.ThrowsAsync<InvalidOperationException>(() => context.GenerateMatchesForNextRound());
        }

        [Theory]
        [InlineData(0, 16)]
        [InlineData(1, 8)]
        [InlineData(2, 4)]
        [InlineData(3, 2)]
        [InlineData(4, 1)]
        public async Task GenerateSecondRound(int numberOfRounds, int resultingMatches)
        {
            using var context = factory.GetContext();
            await context.DeleteEverything();
            await context.AddDummyPlayers(32);
            await context.GenerateMatchesForNextRound();
            for (var i = 0; i < numberOfRounds; i++)
            {
                await context.SetWinnerForAllIncompleteMatches();
                await context.GenerateMatchesForNextRound();
            }

            Assert.Equal(resultingMatches, (await context.GetIncompleteMatches()).Count);
        }
    }
}
