using Microsoft.EntityFrameworkCore;

namespace GenreApi.Models
{
    public class GenreDbContext : DbContext
    {
        public GenreDbContext(DbContextOptions options)
            :base(options)
        {
                
        }

        public DbSet<Genre> Genres { get; set; } = default!;
    }
}
