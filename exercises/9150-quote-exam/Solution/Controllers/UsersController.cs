using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Quotexchange.Api.Data;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

// Note that this controller has not been part of the requirements.
// It is just here to demonstrate the basic principles of authenticated web APIs
// and accessing claims.

namespace Quotexchange.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly QuotexchangeDataContext dataContext;

        public UsersController(QuotexchangeDataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        [HttpGet("me")]
        public async Task<ActionResult<User>> GetCurrentUser()
        {
            var currentUserNameIdentifier = User.Claims.First(u => u.Type == ClaimTypes.NameIdentifier).Value;
            var currentUser = await dataContext.Users.FirstOrDefaultAsync(u => u.NameIdentifier == currentUserNameIdentifier);
            if (currentUser == null) return NotFound();
            return Ok(currentUser);
        }
    }
}
