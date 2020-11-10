using System.Threading.Tasks;

namespace EFIntro
{
    partial class Program
    {
        async static Task WriteToDB(AddressBookContext db) 
        {
            db.Persons.AddRange(new [] {
                new Person() { FirstName = "Tom", LastName = "Turbo" },
                new Person() { FirstName = "Foo", LastName = "Bar" }
            });
            await db.SaveChangesAsync();
        }
    }
}
