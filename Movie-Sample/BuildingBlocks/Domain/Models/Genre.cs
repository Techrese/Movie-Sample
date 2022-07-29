
namespace Domain.Models
{
    public class Genre
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = default!;

        public IEnumerable<Movie> Movies { get; set; } = default!;
    }
}
