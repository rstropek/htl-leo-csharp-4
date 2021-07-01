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
        [Required]
        public string CountryOfOrigin { get; set; } = string.Empty;
    }

    public record NewPirate(
        [MaxLength(150)] string? Name,
        [MaxLength(150)] string? RealName,
        int? YearOfBirth,
        int? YearOfDeath,
        [MaxLength(150)][Required] string CountryOfOrigin);
}
