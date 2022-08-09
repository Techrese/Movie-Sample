
using GenreApi.Models;

namespace GenreApiUnitTesting
{
    public class FakeObjects
    {
        public static IEnumerable<Genre> GetFakeList()
        {
            return new List<Genre>()
            {
                new Genre()
                {
                    Id = Guid.NewGuid(),
                    Name = "Fantasy"
                }
            };
        }
    }
}
