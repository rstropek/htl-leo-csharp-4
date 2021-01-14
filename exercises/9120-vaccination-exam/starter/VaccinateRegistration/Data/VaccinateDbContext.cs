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

        // This class is NOT COMPLETE.
        // Todo: Complete the class according to the requirements

        /// <summary>
        /// Import registrations from JSON file
        /// </summary>
        /// <param name="registrationsFileName">Name of the file to import</param>
        /// <returns>
        /// Collection of all imported registrations
        /// </returns>
        public Task<IEnumerable<Registration>> ImportRegistrations(string registrationsFileName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Delete everything (registrations, vaccinations)
        /// </summary>
        public Task DeleteEverything()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get registration by social security number (SSN) and PIN
        /// </summary>
        /// <param name="ssn">Social Security Number</param>
        /// <param name="pin">PIN code</param>
        /// <returns>
        /// Registration result or null if no registration with given SSN and PIN was found.
        /// </returns>
        public Task<GetRegistrationResult?> GetRegistration(long ssn, int pin)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get available time slots on the given date
        /// </summary>
        /// <param name="date">Date (without time, i.e. time is 00:00:00)</param>
        /// <returns>
        /// Collection of all available time slots
        /// </returns>
        public Task<IEnumerable<DateTime>> GetTimeslots(DateTime date)
        {
            throw new NotImplementedException();
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
        public  Task<Vaccination> StoreVaccination(StoreVaccination vaccination)
        {
            throw new NotImplementedException();
        }
    }
}
