using SpecificatR.Infrastructure.Abstractions;

namespace SpecificationPattern.Demo.CrossCutting.Entities
{
    public class Episode : IBaseEntity<int>
    {
        public Character Hero { get; set; }

        public int Id { get; set; }

        public string Title { get; set; }
    }
}
