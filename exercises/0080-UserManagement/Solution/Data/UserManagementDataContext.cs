using Microsoft.EntityFrameworkCore;

namespace UserManagement.Data
{
    public class UserManagementDataContext : DbContext
    {
        public UserManagementDataContext(DbContextOptions<UserManagementDataContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users => Set<User>();

        public DbSet<Group> Groups => Set<Group>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Add a unique index to name identifier column
            builder.Entity<User>()
                .HasIndex(u => u.NameIdentifier)
                .IsUnique();
        }
    }
}
