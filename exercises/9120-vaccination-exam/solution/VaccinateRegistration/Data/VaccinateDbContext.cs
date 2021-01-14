using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor.

namespace VaccinateRegistration.Data
{
    public record GetRegistrationResult(int Id, long Ssn, string FirstName, string LastName);

    public record StoreVaccination(int RegistrationId, DateTime Datetime);

    public class VaccinateDbContext : DbContext
    {
        public VaccinateDbContext(DbContextOptions<VaccinateDbContext> options) : base(options) { }

        public DbSet<Registration> Registrations { get; set; }

        public DbSet<Vaccination> Vaccinations { get; set; }

        /// <summary>
        /// Import registrations from JSON file
        /// </summary>
        /// <param name="registrationsFileName">Name of the file to import</param>
        /// <returns>
        /// Collection of all imported registrations
        /// </returns>
        public async Task<IEnumerable<Registration>> ImportRegistrations(string registrationsFileName)
        {
            var registrationsText = await File.ReadAllTextAsync(registrationsFileName);
            var registrations = JsonSerializer.Deserialize<IEnumerable<Registration>>(registrationsText)!;

            using var transaction = await Database.BeginTransactionAsync();
            Registrations.AddRange(registrations);
            await SaveChangesAsync();
            await transaction.CommitAsync();

            return registrations;
        }

        /// <summary>
        /// Delete everything (registrations, vaccinations)
        /// </summary>
        public async Task DeleteEverything()
        {
            using var transaction = await Database.BeginTransactionAsync();
            await Database.ExecuteSqlRawAsync("DELETE FROM dbo.Vaccinations");
            await Database.ExecuteSqlRawAsync("DELETE FROM dbo.Registrations");
            await transaction.CommitAsync();
        }

        /// <summary>
        /// Get registration by social security number (SSN) and PIN
        /// </summary>
        /// <param name="ssn">Social Security Number</param>
        /// <param name="pin">PIN code</param>
        /// <returns>
        /// Registration result or null if no registration with given SSN and PIN was found.
        /// </returns>
        public async Task<GetRegistrationResult?> GetRegistration(long ssn, int pin)
            => await Registrations.Where(r => r.SocialSecurityNumber == ssn && r.PinCode == pin)
                .Select(r => new GetRegistrationResult(r.Id, r.SocialSecurityNumber, r.FirstName, r.LastName))
                .FirstOrDefaultAsync();

        /// <summary>
        /// Get available time slots on the given date
        /// </summary>
        /// <param name="date">Date (without time, i.e. time is 00:00:00)</param>
        /// <returns>
        /// Collection of all available time slots
        /// </returns>
        public async Task<IEnumerable<DateTime>> GetTimeslots(DateTime date)
        {
            var vaccinations = await Vaccinations.Where(v => v.VaccinationDate.Year == date.Year
                && v.VaccinationDate.Month == date.Month && v.VaccinationDate.Day == date.Day)
                .Select(v => v.VaccinationDate)
                .ToArrayAsync();

            return Enumerable.Range(0, 3 * 4)
                .Select(n => date.AddHours(8).AddMinutes(15 * n))
                .Where(d => !vaccinations.Any(v => v == d));
        }

        /// <summary>
        /// Store a vaccination
        /// </summary>
        /// <param name="vaccination">Vaccination to store</param>
        /// <returns>
        /// Stored vaccination after it has been written to the database.
        /// </returns>
        /// <remarks>
        /// If a vaccination with the given vaccination.RegistrationID already exists,
        /// overwrite it. Otherwise, insert a new vaccination.
        /// </remarks>
        public async Task<Vaccination> StoreVaccination(StoreVaccination vaccination)
        {
            var vacc = await Vaccinations.FirstOrDefaultAsync(v => v.RegistrationID == vaccination.RegistrationId);
            if (vacc != null)
            {
                vacc.VaccinationDate = vaccination.Datetime;
            }
            else
            {
                vacc = new Vaccination
                {
                    RegistrationID = vaccination.RegistrationId,
                    VaccinationDate = vaccination.Datetime
                };
                Vaccinations.Add(vacc);
            }

            await SaveChangesAsync();
            return vacc;
        }
    }
}
