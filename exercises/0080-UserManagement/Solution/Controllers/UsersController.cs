using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using UserManagement.Data;

namespace UserManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly UserManagementDataContext dc;

        public UsersController(UserManagementDataContext dc)
        {
            this.dc = dc;
        }

        /// <summary>
        /// Returns data of the user who is currently signed in
        /// </summary>
        [HttpGet("me")]
        public async Task<ActionResult<UserResult>> Me()
        {
            // Get user name identifier from user's claims
            var userNameId = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

            // Read user data of current user from DB
            var u = await dc.Users
                .Where(u => u.NameIdentifier == userNameId) // Filter based on name identifier
                .Select(u => new UserResult(u.Id, u.NameIdentifier, u.Email, u.FirstName, u.LastName))
                .FirstAsync();
            return Ok(u);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserResult>>> GetAll([FromQuery] string? filter = null)
        {
            // All users
            IQueryable<User> users = dc.Users;

            // If filter has been provided, apply filter.
            if (filter != null)
            {
                users = users.Where(u => u.Email.Contains(filter)
                    || (u.FirstName != null && u.FirstName.Contains(filter))
                    || (u.LastName != null && u.LastName.Contains(filter)));
            }

            // Read result from database and return
            var result = await users
                .Select(u => new UserResult(u.Id, u.NameIdentifier, u.Email, u.FirstName, u.LastName))
                .ToListAsync();
            return Ok(result);
        }
    }
}
