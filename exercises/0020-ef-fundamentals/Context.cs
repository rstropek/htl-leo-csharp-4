using Microsoft.EntityFrameworkCore;

namespace EFIntro
{
    class AddressBookContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseInMemoryDatabase("AddressBook");
        }
    }
}
