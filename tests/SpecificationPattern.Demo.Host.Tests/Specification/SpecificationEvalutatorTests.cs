using AutoFixture;
using EntityFrameworkCoreMock;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using SpecificationPattern.Demo.Infrastructure;
using SpecificationPattern.Demo.Infrastructure.Repositories;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SpecificationPattern.Demo.Host.Tests.Specification
{
    public class SpecificationEvalutatorTests
    {
        private readonly IFixture _fixture = new Fixture();
        private readonly DbContextOptions<ApplicationContext> _options = new DbContextOptions<ApplicationContext>();

        [Fact]
        public async Task Should_SetCriteria()
        {
            // Arrange
            TestEntity[] entities = _fixture.CreateMany<TestEntity>(2).ToArray();
            var dbContextMock = new DbContextMock<TestDbContext>(_options);
            dbContextMock.CreateDbSetMock(x => x.TestEntities, (x, _) => (x.Id), entities);

            var repository = new ReadAsyncRepository<TestEntity>(dbContextMock.Object);

            // Act
            TestEntity[] result = await repository.GetAllAsync(new TestEntityCriteriaSpecification(entities[0].Id));

            // Assert
            result.Should().NotBeNull().And.BeEquivalentTo(entities[0]);
        }

        [Fact]
        public async Task Should_ApplyPaging()
        {
            // Arrange
            TestEntity[] entities = _fixture.CreateMany<TestEntity>(4).ToArray();
            var dbContextMock = new DbContextMock<TestDbContext>(_options);
            dbContextMock.CreateDbSetMock(x => x.TestEntities, (x, _) => (x.Id), entities);

            var repository = new ReadAsyncRepository<TestEntity>(dbContextMock.Object);

            // Act
            TestEntity[] result = await repository.GetAllAsync(new TestEntityPaginatedSpecification(1, 2));

            // Assert
            result.Should().HaveCount(2);
            result[0].Id.Should().Equals(entities[0]);
            result[1].Id.Should().Equals(entities[1]);
        }

        [Fact]
        public async Task Should_OrderByNameAscending()
        {
            // Arrange
            TestEntity[] entities = _fixture.CreateMany<TestEntity>(4).ToArray();
            var dbContextMock = new DbContextMock<TestDbContext>(_options);
            dbContextMock.CreateDbSetMock(x => x.TestEntities, (x, _) => (x.Id), entities);

            var repository = new ReadAsyncRepository<TestEntity>(dbContextMock.Object);

            // Act
            TestEntity[] result = await repository.GetAllAsync(new TestEntityOrderByNameAscSpecification());
            TestEntity[] orderedList = entities.OrderBy(o => o.Name).ToArray();

            // Assert
            result.Should().Equals(orderedList);
        }

        [Fact]
        public async Task Should_OrderByNameDescending()
        {
            // Arrange
            TestEntity[] entities = _fixture.CreateMany<TestEntity>(4).ToArray();
            var dbContextMock = new DbContextMock<TestDbContext>(_options);
            dbContextMock.CreateDbSetMock(x => x.TestEntities, (x, _) => (x.Id), entities);

            var repository = new ReadAsyncRepository<TestEntity>(dbContextMock.Object);

            // Act
            TestEntity[] result = await repository.GetAllAsync(new TestEntityOrderByNameDescSpecification());
            TestEntity[] orderedList = entities.OrderByDescending(o => o.Name).ToArray();

            // Assert
            result.Should().Equals(orderedList);
        }
    }
}
