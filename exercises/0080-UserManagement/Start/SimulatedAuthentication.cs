using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

// This file contains classes used to simulate user authentication. It is
// provided by your teacher. You do not need to change it in any way. In
// practice, you would not use this simulated authentication. Instead, you
// would use *OpenID Connect*-based authentication with e.g. *Azure Active
// Directory* (we did that during our C# course).

namespace UserManagement
{
    public class SimulatedAuthenticationOptions : AuthenticationSchemeOptions
    {
        public string UserNameIdentifier { get; set; } = string.Empty;

        public string UserRole { get; set; } = string.Empty;

        public const string AuthScheme = "Simulated";
    }

    public class SimulatedAuthenticationHandler : AuthenticationHandler<SimulatedAuthenticationOptions>
    {
        public SimulatedAuthenticationHandler(IOptionsMonitor<SimulatedAuthenticationOptions> options,
            ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
            : base(options, logger, encoder, clock) { }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, Options.UserNameIdentifier),
                new Claim(ClaimTypes.Role, Options.UserRole)
            };

            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims, Scheme.Name));
            var ticket = new AuthenticationTicket(claimsPrincipal,
                new AuthenticationProperties { IsPersistent = false }, Scheme.Name);
            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
    }

    public static class SimulatedAuthenticationExtensions
    {
        public static AuthenticationBuilder AddSimulatedAuthentication(
            this AuthenticationBuilder builder, string userNameidentifier,
            string userRole)
        {
            builder.AddScheme<SimulatedAuthenticationOptions, SimulatedAuthenticationHandler>(
                SimulatedAuthenticationOptions.AuthScheme,
                options =>
                {
                    options.UserNameIdentifier = userNameidentifier;
                    options.UserRole = userRole;
                });
            return builder;
        }
    }
}
