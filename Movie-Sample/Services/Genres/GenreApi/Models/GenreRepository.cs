using GenreApi.Models.Abstractions;

namespace GenreApi.Models
{
    public class GenreRepository : IGenreRepository
    {
        public Task<bool> AddGenreAsync(Genre genre)
        {
            throw new NotImplementedException();
        }

        public void DeleteGenre(Genre genre)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Genre>> GetAllGenreAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Genre> GetGenreAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool UpdateGenre(Genre genre)
        {
            throw new NotImplementedException();
        }
    }
}
