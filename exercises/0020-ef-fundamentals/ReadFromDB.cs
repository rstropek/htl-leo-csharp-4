using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace EFIntro
{
    partial class Program
    {
        async static Task ReadFromDB(AddressBookContext db) 
        {
            await foreach(var person in db.Persons
                .Where(p => p.LastName.StartsWith("B"))
                .AsAsyncEnumerable())
            {
                Console.WriteLine($"{person.LastName}, {person.FirstName}");
            }
        }
    }
}
