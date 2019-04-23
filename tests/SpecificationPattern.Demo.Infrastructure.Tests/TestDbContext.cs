using Microsoft.EntityFrameworkCore;
using SpecificationPattern.Demo.CrossCutting.Entities;

namespace SpecificationPattern.Demo.Infrastructure.Tests
{
    public class TestDbContext : ApplicationContext
    {
        public TestDbContext(DbContextOptions<ApplicationContext> options)
            : base(options, null)
        {
        }

        public virtual DbSet<TestEntity> TestEntities { get; set; }
    }

    public class TestEntity : IBaseEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Number { get; set; }
    }
}
