using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TournamentPlanner.Data;
using Xunit;

namespace TournamentPlannerTests
{
    /// <summary>
    /// Test cases for basic requirements
    /// </summary>
    public class DbContextBasicsTests : IClassFixture<DbContextFactory>
    {
        private readonly DbContextFactory factory;

        public DbContextBasicsTests(DbContextFactory factory) => this.factory = factory;

        [Fact]
        public async Task CreateDbContextAndOpenConnection()
        {
            using var context = factory.GetContext();
            await context.Database.OpenConnectionAsync();
        }

        [Fact]
        public async Task DeleteEverythingDoesNotThrow()
        {
            using var context = factory.GetContext();
            await context.DeleteEverything();
        }

        [Fact]
        public async Task AddClubMemberDoesNotThrow()
        {
            using var context = factory.GetContext();
            await context.DeleteEverything();
            var p = await context.AddPlayer(new() { Name = "Foo Bar" });
            Assert.NotNull(p);
            Assert.NotEqual(0, p.ID);
        }

        [Fact]
        public async Task AddMatchDoesNotThrow()
        {
            using var context = factory.GetContext();
            await context.DeleteEverything();
            var foo = await context.AddPlayer(new() { Name = "Foo" });
            var bar = await context.AddPlayer(new() { Name = "Bar" });
            var m = await context.AddMatch(foo.ID, bar.ID, 1);
            Assert.NotNull(m);
            Assert.NotEqual(0, m.ID);
        }

        [Fact]
        public async Task SetWinnerDoesNotThrow()
        {
            using var context = factory.GetContext();
            await context.DeleteEverything();
            var foo = await context.AddPlayer(new() { Name = "Foo" });
            var bar = await context.AddPlayer(new() { Name = "Bar" });
            var m = await context.AddMatch(foo.ID, bar.ID, 1);
            await context.SetWinner(m.ID, PlayerNumber.Player1);
        }

        [Fact]
        public async Task GetIncompleteMatchesDoesNotThrow()
        {
            using var context = factory.GetContext();
            await context.DeleteEverything();
            var foo = await context.AddPlayer(new() { Name = "Foo" });
            var bar = await context.AddPlayer(new() { Name = "Bar" });
            await context.AddMatch(foo.ID, bar.ID, 1);
            var m = await context.GetIncompleteMatches();
            Assert.Single(m);
        }
    }
}
