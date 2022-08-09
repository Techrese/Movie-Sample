using System.ComponentModel.DataAnnotations;

namespace GenreApi.Dtos
{
    public class GenreDto
    {
        [Required]
        public string Name { get; set; } = default!;
    }
}
