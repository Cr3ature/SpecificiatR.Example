using SpecificatR.Infrastructure.Abstractions;
using System.Collections.Generic;

namespace SpecificationPattern.Demo.CrossCutting.Entities
{
    public class Planet : IBaseEntity<int>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Terrain { get; set; }

        public string Climate { get; set; }

        public string Population { get; set; }

        public ICollection<Human> Humans { get; set; }
    }
}
