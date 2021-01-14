using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace VaccinateRegistration.Data
{
    public class Registration
    {
        public int Id { get; set; }

        [JsonPropertyName("ssn")]
        public long SocialSecurityNumber { get; set; }

        [JsonPropertyName("pin")]
        public int PinCode { get; set; }

        [JsonPropertyName("firstName")]
        [MaxLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [JsonPropertyName("lastName")]
        [MaxLength(100)]
        public string LastName { get; set; } = string.Empty;

        // This class is NOT COMPLETE.
        // Todo: Complete the class according to the requirements
    }
}
