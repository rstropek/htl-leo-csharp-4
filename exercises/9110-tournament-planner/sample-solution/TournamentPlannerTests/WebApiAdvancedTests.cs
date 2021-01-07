using Microsoft.AspNetCore.Mvc.Testing;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TournamentPlanner;
using TournamentPlanner.Data;
using Xunit;

namespace TournamentPlannerTests
{
    /// <summary>
    /// Test cases for advanced requirements
    /// </summary>
    public class WebApiAdvancedTests : IClassFixture<WebApplicationFactory<Startup>>, IClassFixture<DbContextFactory>
    {
        private readonly WebApplicationFactory<Startup> webClientFactory;
        private readonly DbContextFactory contextFactory;

        public WebApiAdvancedTests(WebApplicationFactory<Startup> webClientFactory, DbContextFactory contextFactory) => 
            (this.webClientFactory, this.contextFactory) = (webClientFactory, contextFactory);

        [Fact]
        public async Task AddPlayer()
        {
            using var context = contextFactory.GetContext();
            await context.DeleteEverything();

            using var client = webClientFactory.CreateClient();
            var response = await client.PostAsJsonAsync("/api/players", new Player() { Name = "Foo" });
            response.EnsureSuccessStatusCode();

            Assert.Single(await context.GetFilteredPlayers());
        }

        [Theory]
        [InlineData("", true)]
        [InlineData("?name=Foo", true)]
        [InlineData("?name=Baz", false)]
        public async Task GetFilteredPlayers(string filter, bool expectRows)
        {
            using var context = contextFactory.GetContext();
            await context.DeleteEverything();
            await context.AddPlayer(new() { Name = "FooBar" });

            using var client = webClientFactory.CreateClient();
            var p = await client.GetFromJsonAsync<IEnumerable<Player>>($"/api/players{filter}");
            Assert.NotNull(p);
            if (expectRows)
            {
                Assert.Single(p);
                Assert.Equal("FooBar", p.First().Name);
            }
            else
            {
                Assert.Empty(p);
            }
        }

        [Fact]
        public async Task GetIncompleteMatches()
        {
            using var context = contextFactory.GetContext();
            await context.DeleteEverything();
            var foo = await context.AddPlayer(new() { Name = "Foo" });
            var bar = await context.AddPlayer(new() { Name = "Bar" });
            var dbm = await context.AddMatch(foo.ID, bar.ID, 1);

            using var client = webClientFactory.CreateClient();
            var m = await client.GetFromJsonAsync<IEnumerable<Match>>("/api/matches/open");
            Assert.Single(m);
            Assert.Equal(dbm.ID, m.First().ID);
        }

        [Fact]
        public async Task GenerateFirstRound()
        {
            using var context = contextFactory.GetContext();
            await context.DeleteEverything();
            await context.AddDummyPlayers(32);

            using var client = webClientFactory.CreateClient();
            var response = await client.PostAsync("/api/matches/generate", null);
            response.EnsureSuccessStatusCode();

            Assert.Equal(16, (await context.GetIncompleteMatches()).Count);
        }
    }
}
