using SpecificationPattern.Demo.CrossCutting.Entities;
using SpecificatR.Infrastructure.Abstractions;

namespace SpecificationPattern.Demo.Host.Specifications
{
    public class OrderEpisodesOnNameSpecification : BaseSpecification<Episode>
    {
        public OrderEpisodesOnNameSpecification()
            : base(null)
        {
            AddOrderBy(o => o.Title, OrderByDirection.Ascending);
        }
    }
}
