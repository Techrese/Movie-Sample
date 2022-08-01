using GenreApi.Models.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GenreApi.Models
{

    public class GenreRepository : IGenreRepository
    {
        private readonly GenreDbContext _context;
        private readonly ILogger<GenreRepository> _logger;

        public GenreRepository(GenreDbContext context, ILogger<GenreRepository> logger)
        {
            _context = context;
            _logger = logger;   
        }

        public async ValueTask<bool> AddGenreAsync(Genre genre)
        {
            await _context.Genres.AddAsync(genre);
            if (await _context.SaveChangesAsync() > 0)
            {
                return true;
            }
            return false;
        }

        public void DeleteGenre(Genre genre)
        {
            _context.Genres.Remove(genre);
        }

        public async ValueTask<IEnumerable<Genre>> GetAllGenreAsync()
        {
            return await _context.Genres.ToListAsync();
        }

        public async ValueTask<Genre> GetGenreAsync(Guid id)
        {
            return await _context.Genres.FindAsync(id);
        }

        public async ValueTask<Genre> SearchGenre(Expression<Func<Genre, bool>> predicate)
        {
            return await _context.Genres.Where(predicate).FirstOrDefaultAsync();
        }

        public void UpdateGenre(Genre genre)
        {
            _context.Genres.Update(genre);
        }
    }
}
