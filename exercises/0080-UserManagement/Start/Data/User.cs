using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UserManagement.Data
{
    /// <summary>
    /// Represents a user in the database
    /// </summary>
    public class User
    {
        public int Id { get; set; }

        [MaxLength(128)]
        public string NameIdentifier { get; set; } = string.Empty;

        [MaxLength(256)]
        public string Email { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? FirstName { get; set; }

        [MaxLength(100)]
        public string? LastName { get; set; }
    }
}
