using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TournamentPlanner.Data;
using Xunit;

// Disable parallel test execution because this test suite
// contains integration tests accessing a real database (LocalDB).
[assembly: CollectionBehavior(DisableTestParallelization = true)]

namespace TournamentPlannerTests
{
    /// <summary>
    /// Factory for EFCore DB context instances.
    /// </summary>
    /// <remarks>
    /// To be used with xUnit's <see cref="IClassFixture{TFixture}"/>.
    /// </remarks>
    public sealed class DbContextFactory
    {
        private readonly DbContextOptions<TournamentPlannerDbContext> options;

        public DbContextFactory()
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            var optionsBuilder = new DbContextOptionsBuilder<TournamentPlannerDbContext>();
            optionsBuilder.UseSqlServer(configuration["ConnectionStrings:DefaultConnection"]);
            options = optionsBuilder.Options;
        }

        public TournamentPlannerDbContext GetContext() => new(options);
    }
}
