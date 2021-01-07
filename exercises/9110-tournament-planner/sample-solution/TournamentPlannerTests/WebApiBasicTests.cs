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
    /// Test cases for basic requirements
    /// </summary>
    public class WebApiBasicTests : IClassFixture<WebApplicationFactory<Startup>>, IClassFixture<DbContextFactory>
    {
        private readonly WebApplicationFactory<Startup> webClientFactory;
        private readonly DbContextFactory contextFactory;

        public WebApiBasicTests(WebApplicationFactory<Startup> webClientFactory, DbContextFactory contextFactory) => 
            (this.webClientFactory, this.contextFactory) = (webClientFactory, contextFactory);

        [Fact]
        public async Task GetAllPlayers()
        {
            using var context = contextFactory.GetContext();
            await context.DeleteEverything();
            await context.AddDummyPlayers(5);

            using var client = webClientFactory.CreateClient();
            var p = await client.GetFromJsonAsync<IEnumerable<Player>>("/api/players");
            Assert.NotNull(p);
            Assert.Equal(5, p.Count());
        }
    }
}
