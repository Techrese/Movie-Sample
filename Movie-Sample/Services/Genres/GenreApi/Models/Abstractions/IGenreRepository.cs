using System.Linq.Expressions;

namespace GenreApi.Models.Abstractions
{
    public interface IGenreRepository
    {
        ValueTask<IEnumerable<Genre>> GetAllGenreAsync();
        ValueTask<Genre> GetGenreAsync(Guid id);
        void DeleteGenre(Genre genre);
        ValueTask<bool> AddGenreAsync(Genre genre);
        void UpdateGenre(Genre genre);
        ValueTask<Genre> SearchGenre(Expression<Func<Genre, bool>> predicate);
    }
}
