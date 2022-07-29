namespace Domain.Models
{
    public class Actor
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = default!;

        public string ProfileUrl { get; set; } = default!;

        public IEnumerable<Movie> Movies { get; set; } = default!;

    }
}
