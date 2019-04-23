using AutoFixture;
using EntityFrameworkCoreMock;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using SpecificationPattern.Demo.CrossCutting.Entities;
using SpecificationPattern.Demo.Host.Specifications;
using SpecificationPattern.Demo.Infrastructure;
using SpecificationPattern.Demo.Infrastructure.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SpecificationPattern.Demo.Host.Tests.Specification
{
    public class SpecificationTests
    {
        private readonly IFixture _fixture = new Fixture();
        private readonly DbContextOptions<ApplicationContext> _options = new DbContextOptions<ApplicationContext>();

        [Fact]
        public async Task WithFriendsFromTatooineSpecification_ShouldApplySpecification()
        {
            // Arrange
            var tatooineHomeplant = _fixture.Build<Planet>()
                .Without(wh => wh.Humans).Create();
            tatooineHomeplant.Name = "Tatooine";

            var baseCharacter = _fixture.Build<Character>()
                .Without(wh => wh.CharacterEpisodes)
                .Without(wh => wh.CharacterFriends)
                .Without(wh => wh.FriendCharacters).Create();

            var friendCharacter = _fixture.Build<Human>()
                .Without(wh => wh.CharacterEpisodes)
                .Without(wh => wh.CharacterFriends)
                .Without(wh => wh.FriendCharacters)
                .Without(wh => wh.HomePlanet).Create();
            friendCharacter.HomePlanet = tatooineHomeplant;

            baseCharacter.CharacterFriends = new CharacterFriend[]
            {
                new CharacterFriend
                {
                    Character = baseCharacter,
                    CharacterId = baseCharacter.Id,
                    Friend = friendCharacter,
                    FriendId = friendCharacter.Id
                }
            };

            Character[] entities = new Character[] 
            {
                baseCharacter
            };

            var dbContextMock = new DbContextMock<TestDbContext>(_options);
            dbContextMock.CreateDbSetMock(x => x.Characters, (x, _) => (x.Id), entities);

            var repository = new ReadAsyncRepository<Character>(dbContextMock.Object);

            // Act
            Character[] characters = await repository.GetAllAsync(new WithFriendsFromTatooineSpecification());

            // Assert
            characters.Should().NotBeNull().And.NotBeEmpty();
        }

        [Fact]
        public async Task WithFriendsFromTatooineSpecification_WithOutValidMembers_ShouldApplySpecification()
        {
            // Arrange
            var notTatooineHomeplant = _fixture.Build<Planet>()
                .Without(wh => wh.Humans).Create();
            notTatooineHomeplant.Name = "Earth";

            var baseCharacter = _fixture.Build<Character>()
                .Without(wh => wh.CharacterEpisodes)
                .Without(wh => wh.CharacterFriends)
                .Without(wh => wh.FriendCharacters).Create();

            var friendCharacter = _fixture.Build<Human>()
                .Without(wh => wh.CharacterEpisodes)
                .Without(wh => wh.CharacterFriends)
                .Without(wh => wh.FriendCharacters)
                .Without(wh => wh.HomePlanet).Create();
            friendCharacter.HomePlanet = notTatooineHomeplant;

            baseCharacter.CharacterFriends = new CharacterFriend[]
            {
                new CharacterFriend { Character = baseCharacter, CharacterId = baseCharacter.Id, Friend = friendCharacter, FriendId = friendCharacter.Id }
            };

            Character[] entities = new Character[]
            {
                baseCharacter
            };

            var dbContextMock = new DbContextMock<TestDbContext>(_options);
            dbContextMock.CreateDbSetMock(x => x.Characters, (x, _) => (x.Id), entities);

            var repository = new ReadAsyncRepository<Character>(dbContextMock.Object);
            // Act
            Character[] characters = await repository.GetAllAsync(new WithFriendsFromTatooineSpecification());

            // Assert
            characters.Should().NotBeNull().And.BeEmpty();
        }

        [Fact]
        public async Task CharactersOfTypeHumanSpecification_ShouldApplySpecification()
        {
            // Arrange
            var baseCharacters = _fixture.Build<Character>()
                .Without(wh => wh.CharacterEpisodes)
                .Without(wh => wh.CharacterFriends)
                .Without(wh => wh.FriendCharacters).CreateMany(2);

            var humanCharacters = _fixture.Build<Human>()
                .Without(wh => wh.CharacterEpisodes)
                .Without(wh => wh.CharacterFriends)
                .Without(wh => wh.FriendCharacters)
                .Without(wh => wh.HomePlanet).CreateMany(2);

            List<Character> characters = new List<Character>();
            characters.AddRange(baseCharacters);
            characters.AddRange(humanCharacters);

            Character[] entities = characters.ToArray();

            var dbContextMock = new DbContextMock<TestDbContext>(_options);
            var set = dbContextMock.CreateDbSetMock(x => x.Characters, (x, _) => (x.Id), entities);

            var repository = new ReadAsyncRepository<Character>(dbContextMock.Object);

            // Act
            Character[] charactersResult = await repository.GetAllAsync(new CharactersOfTypeHumanSpecification());

            // Assert
            charactersResult.Should().NotBeNull().And.HaveSameCount(humanCharacters);
        }
    }
}
