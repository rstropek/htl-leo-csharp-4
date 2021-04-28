using System.ComponentModel.DataAnnotations;

namespace Quotexchange.Api.Data
{
    public enum UpDown
    {
        Down = -1,
        Up = 1
    };

    public class Vote
    {
        public int Id { get; set; }

        [Required]
        public FunnyQuote? Quote { get; set; }

        [Required]
        public int QuoteId { get; set; }

        public UpDown UpDown { get; set; }
    }
}
