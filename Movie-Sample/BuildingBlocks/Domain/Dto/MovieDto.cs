
namespace Domain.Dto
{
    public class MovieDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = default!;

        public string Description { get; set; } = default!;
    }
}
