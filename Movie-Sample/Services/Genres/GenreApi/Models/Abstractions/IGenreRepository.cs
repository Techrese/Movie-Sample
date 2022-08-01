namespace GenreApi.Models.Abstractions
{
    public interface IGenreRepository
    {
        Task<IEnumerable<Genre>> GetAllGenreAsync();
        Task<Genre> GetGenreAsync(Guid id);
        void DeleteGenre(Genre genre);
        Task<bool> AddGenreAsync(Genre genre);
        bool UpdateGenre(Genre genre);
    }
}
