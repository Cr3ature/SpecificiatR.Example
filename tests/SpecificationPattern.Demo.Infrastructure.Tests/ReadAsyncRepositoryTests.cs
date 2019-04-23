using AutoFixture;
using EntityFrameworkCoreMock;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using SpecificationPattern.Demo.CrossCutting.Interfaces;
using SpecificationPattern.Demo.Infrastructure.Repositories;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SpecificationPattern.Demo.Infrastructure.Tests
{
    public class ReadAsyncRepositoryTests
    {
        private readonly IFixture _fixture = new Fixture();
        private readonly DbContextOptions<ApplicationContext> _options = new DbContextOptions<ApplicationContext>();

        [Fact]
        public async Task GetById_ShouldReturnEntity()
        {
            // Arrange
            TestEntity[] entities = _fixture.CreateMany<TestEntity>(2).ToArray();

            var dbContextMock = new DbContextMock<TestDbContext>(_options);
            dbContextMock.CreateDbSetMock(x => x.TestEntities, (x, _) => (x.Id), entities);

            var repository = new ReadAsyncRepository<TestEntity>(dbContextMock.Object);

            // Act
            TestEntity result = await repository.GetByIdAsync(entities[0].Id);

            // Assert
            result.Should().NotBeNull().And.BeEquivalentTo(entities[0]);
        }

        [Fact]
        public async Task GetById_UnknownId_ShouldReturnNull()
        {
            // Arrange
            TestEntity[] entities = _fixture.CreateMany<TestEntity>(2).ToArray();

            var dbContextMock = new DbContextMock<TestDbContext>(_options);
            dbContextMock.CreateDbSetMock(x => x.TestEntities, (x, _) => (x.Id), entities);

            var repository = new ReadAsyncRepository<TestEntity>(dbContextMock.Object);

            // Act
            TestEntity result = await repository.GetByIdAsync(0);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetByIdAsyncWithSpecification_ShouldApplySpecification()
        {
            // Arrange
            TestEntity[] entities = _fixture.CreateMany<TestEntity>(2).ToArray();

            var dbContextMock = new DbContextMock<TestDbContext>(_options);
            dbContextMock.CreateDbSetMock(x => x.TestEntities, (x, _) => (x.Id), entities);
            var repository = new ReadAsyncRepository<TestEntity>(dbContextMock.Object);

            var specification = new Mock<ISpecification<TestEntity>>();

            // Act
            TestEntity result = await repository.GetSingleAsync(specification.Object);

            // Assert
            result.Should().NotBeNull().And.BeOfType(typeof(TestEntity));
        }

        [Fact]
        public async Task ListAllAsync_ShouldReturnEntities()
        {
            // Arrange
            TestEntity[] entities = _fixture.CreateMany<TestEntity>(2).ToArray();

            var dbContextMock = new DbContextMock<TestDbContext>(_options);
            dbContextMock.CreateDbSetMock(x => x.TestEntities, (x, _) => (x.Id), entities);
            var repository = new ReadAsyncRepository<TestEntity>(dbContextMock.Object);

            // Act
            TestEntity[] result = await repository.GetAllAsync();

            // Assert
            result.Should().NotBeNull().And.HaveCount(2);
        }

        [Fact]
        public async Task ListAllAsyncWithSpecification_ShouldApplySpecification()
        {
            // Arrange
            TestEntity[] entities = _fixture.CreateMany<TestEntity>(2).ToArray();

            var dbContextMock = new DbContextMock<TestDbContext>(_options);
            dbContextMock.CreateDbSetMock(x => x.TestEntities, (x, _) => (x.Id), entities);
            var repository = new ReadAsyncRepository<TestEntity>(dbContextMock.Object);

            var specification = new Mock<ISpecification<TestEntity>>();

            // Act
            TestEntity[] result = await repository.GetAllAsync(specification.Object);

            // Assert
            result.Should().NotBeNull().And.BeOfType(typeof(TestEntity[]));
        }
    }
}
