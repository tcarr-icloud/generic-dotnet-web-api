using Microsoft.EntityFrameworkCore;

namespace webapi
{
    public class DatabaseApiContext : DbContext
    {
        public DatabaseApiContext(DbContextOptions<DatabaseApiContext> options) : base(options)
        {
        }

        public DbSet<User> User { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<Workspace> Workspace { get; set; }
    }
}