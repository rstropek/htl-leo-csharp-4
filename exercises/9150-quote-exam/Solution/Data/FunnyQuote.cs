using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Quotexchange.Api.Data
{
    public class FunnyQuote
    {
        public int Id { get; set; }

        [Required]
        public User? Creator { get; set; }

        [Required]
        public int CreatorId { get; set; }

        [MinLength(5)]
        [MaxLength(500)]
        public string Quote { get; set; } = string.Empty;

        [MinLength(1)]
        [MaxLength(150)]
        public string Source { get; set; } = string.Empty;

        public List<Vote>? Votes { get; set; }
    }
}
