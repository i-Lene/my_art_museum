using Microsoft.EntityFrameworkCore;

namespace backend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> users { get; set; }
        public DbSet<Favourites> favourites { get; set; }
    }

    public class User
    {
        public int id { get; set; }
        public string? name { get; set; }
        public string? email { get; set; }
    }

    public class Favourites
    {
        public int id { get; set; }
        public int userId { get; set; }
        public int artworkId { get; set; }
    }


}
