using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace UserManagement.Data
{
    /// <summary>
    /// Provides methods for filling the database with demo data
    /// </summary>
    public class DemoDataGenerator
    {
        private readonly UserManagementDataContext dc;

        public DemoDataGenerator(UserManagementDataContext dc)
        {
            this.dc = dc;
        }

        /// <summary>
        /// Delete all data in the database
        /// </summary>
        /// <returns></returns>
        public async Task ClearAll()
        {
            dc.Users.RemoveRange(await dc.Users.ToArrayAsync());
            dc.Groups.RemoveRange(await dc.Groups.ToArrayAsync());
            await dc.SaveChangesAsync();
        }

        /// <summary>
        /// Fill database with demo data
        /// </summary>
        public async Task Fill()
        {
            #region Add some users
            User foo, john, jane;
            dc.Users.Add(foo = new User
            {
                NameIdentifier = "foo.bar",
                FirstName = "Foo",
                LastName = "Bar",
                Email = "foo.bar@acme.corp"
            });

            dc.Users.Add(john = new User
            {
                NameIdentifier = "john.doe",
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@acme.corp"
            });

            dc.Users.Add(jane = new User
            {
                NameIdentifier = "jane.doe",
                FirstName = "Jane",
                LastName = "Doe",
                Email = "jane.doe@acme.corp"
            });
            #endregion

            #region Add some groups
            Group all, mgmt, rd, sales;
            dc.Groups.Add(all = new Group { Name = "All" });

            // The following groups are members of the *All* group
            dc.Groups.Add(mgmt = new Group { Name = "Management", ParentGroup = all });
            dc.Groups.Add(rd = new Group { Name = "R&D", ParentGroup = all });
            dc.Groups.Add(sales = new Group { Name = "Sales", ParentGroup = all });

            // Assign users to groups
            foo.Memberships = new() { sales };
            john.Memberships = new() { rd };
            jane.Memberships = new() { rd, mgmt };
            #endregion

            await dc.SaveChangesAsync();
        }
    }
}
