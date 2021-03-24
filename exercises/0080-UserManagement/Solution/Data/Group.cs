using System.Collections.Generic;

namespace UserManagement.Data
{
    /// <summary>
    /// Represents a user group in the database
    /// </summary>
    public class Group
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// List of users that are members of this group
        /// </summary>
        public List<User>? UserMembers { get; set; }

        /// <summary>
        /// List of groups that are members of this group
        /// </summary>
        public List<Group>? GroupMembers { get; set; }

        /// <summary>
        /// Parent group for this group
        /// </summary>
        public Group? ParentGroup { get; set; }

        public int? ParentGroupId { get; set; }
    }
}
