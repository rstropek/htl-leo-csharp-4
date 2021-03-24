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
    [Authorize(Roles = "administrator")] // Note authorization based on role claim
    public class GroupsController : ControllerBase
    {
        private readonly UserManagementDataContext dc;

        public GroupsController(UserManagementDataContext dc)
        {
            this.dc = dc;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GroupResult>>> GetAll()
        {
            // This is how you would check role membership manually. However,
            // in this case, using the `[Authorize(Roles = "administrator")]` shown above
            // should be preferred.

            //if (User.Claims.First(c => c.Type == ClaimTypes.Role).Value != "administrator")
            //{
            //    return Forbid();
            //}

            // Return all groups, no filtering
            return Ok(await dc.Groups.Select(g => new GroupResult(g.Id, g.Name)).ToListAsync());
        }

        [HttpGet("{id}", Name = nameof(GetSingleGroup))]
        public async Task<ActionResult<GroupResult>> GetSingleGroup(int id)
        {
            var g = await dc.Groups
                .Where(g => g.Id == id) // Filter based on given group id
                .Select(g => new GroupResult(g.Id, g.Name))
                .FirstOrDefaultAsync();
            if (g == null) return NotFound();
            return Ok(g);
        }

        [HttpGet("{id}/users")]
        public async Task<ActionResult<UserResult>> GetMemberUsers(int id, [FromQuery] bool recursive = false)
        {
            // Verify that group with given ID exists
            if (!await dc.Groups.AnyAsync(g => g.Id == id)) return NotFound();

            // Get user members, if necessary recursively
            var users = await GetMemberUsersImpl(id, recursive);

            return Ok(users.Select(u => new UserResult(u.Id, u.NameIdentifier, u.Email, u.FirstName, u.LastName)));
        }

        private async Task<IEnumerable<User>> GetMemberUsersImpl(int groupId, bool recursive)
        {
            // Get group with given id. Note that we include user and group members.
            var group = await dc.Groups
                .Include(g => g.UserMembers)
                .Include(g => g.GroupMembers)
                .Where(g => g.Id == groupId)
                .Select(g => new { g.UserMembers, g.GroupMembers })
                .FirstAsync();

            // Add all direct user members
            List<User> users = new();
            users.AddRange(group.UserMembers!);

            // If recursive lookup is not necessary, we are done
            if (!recursive) return users;

            // Loop through all group members and find user members recursively
            foreach (var child in group.GroupMembers!)
            {
                users.AddRange(await GetMemberUsersImpl(child.Id, recursive));
            }

            // Make sure that result does not contain duplicate members
            return users.Distinct();
        }

        [HttpGet("{id}/groups")]
        public async Task<ActionResult<GroupResult>> GetMemberGroups(int id)
        {
            // Get group with given id. Note that we include group members.
            var g = await dc.Groups.Include(g => g.GroupMembers).FirstOrDefaultAsync(g => g.Id == id);
            if (g == null) return NotFound();
            return Ok(g.GroupMembers!.Select(g2 => new GroupResult(g2.Id, g2.Name)));
        }
    }
}
