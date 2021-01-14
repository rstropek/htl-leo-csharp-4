using System;
using System.ComponentModel.DataAnnotations;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor.

namespace VaccinateRegistration.Data
{
    public class Vaccination
    {
        public int Id { get; set; }

        // This class is NOT COMPLETE.
        // Todo: Complete the class according to the requirements

        public DateTime VaccinationDate { get; set; }
    }
}
