using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EFIntro
{
    partial class Program
    {
        static void Main(string[] args)
        {
            using (var db = new AddressBookContext())
            {
                WriteToDB(db).Wait();
                ReadFromDB(db).Wait();
            }
        }
    }
}
