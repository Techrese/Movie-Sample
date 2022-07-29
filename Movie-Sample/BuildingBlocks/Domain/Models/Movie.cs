namespace Domain.Models
{
    public class Movie
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = default!;

        public string Description { get; set; } = default!;

        public IEnumerable<Actor> Actors { get; set; } = default!;

        public IEnumerable<Genre> Genres { get; set; } = default!;
    }
}
