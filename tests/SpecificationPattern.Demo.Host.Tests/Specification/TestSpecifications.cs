using SpecificationPattern.Demo.Host.Specifications;
using System;
using System.Linq.Expressions;

namespace SpecificationPattern.Demo.Host.Tests.Specification
{
    public class TestEntityOrderByNameAscSpecification : BaseSpecification<TestEntity>
    {
        public TestEntityOrderByNameAscSpecification()
            : base(null)
        {
            AddOrderBy(o => o.Name, CrossCutting.Enums.OrderByDirection.Ascending);
        }
    }

    public class TestEntityOrderByNameDescSpecification : BaseSpecification<TestEntity>
    {
        public TestEntityOrderByNameDescSpecification()
            : base(null)
        {
            AddOrderBy(o => o.Name, CrossCutting.Enums.OrderByDirection.Descending);
        }
    }

   
    public class TestEntityCriteriaSpecification : BaseSpecification<TestEntity>
    {
        public TestEntityCriteriaSpecification(int TestEntityId)
            : base(BuildCriteria(TestEntityId))
        {
        }

        private static Expression<Func<TestEntity, bool>> BuildCriteria(int TestEntityId)
        {
            return x => x.Id == TestEntityId;
        }
    }

    public class TestEntityPaginatedSpecification : BaseSpecification<TestEntity>
    {
        public TestEntityPaginatedSpecification(int pageIndex, int pageSize)
            : base(null)
        {
            ApplyPaging(pageIndex, pageSize);
        }
    }
}
