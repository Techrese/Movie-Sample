namespace Domain.Dto
{
    public class ActorDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = default!;

        public string ProfileUrl { get; set; } = default!;
    }
}
