using SpecificationPattern.Demo.CrossCutting.Entities;
using SpecificatR.Infrastructure.Abstractions;
using System;
using System.Linq.Expressions;

namespace SpecificationPattern.Demo.Host.Specifications
{
    public class PagedEpisodesSpecification : BaseSpecification<Episode>
    {
        public PagedEpisodesSpecification(int pageIndex, int pageSize)
            : base(BuildCriteria())
        {
            ApplyPaging(pageIndex, pageSize);
        }

        private static Expression<Func<Episode, bool>> BuildCriteria() => null;
    }
}
