using System;
using System.ComponentModel.DataAnnotations;

namespace Pirates.Model
{
    public class Pirate
    {
        [Key]
        public Guid ID { get; set; } = Guid.NewGuid();

        [MaxLength(150)]
        public string? Name { get; set; }

        [MaxLength(150)]
        public string? RealName { get; set; } = string.Empty;

        public int? YearOfBirth { get; set; }

        public int? YearOfDeath { get; set; }

        [MaxLength(150)]
        public string CountryOfOrigin { get; set; } = string.Empty;
    }
}
