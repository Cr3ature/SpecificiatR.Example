using SpecificationPattern.Demo.CrossCutting.Entities;
using SpecificationPattern.Demo.Host.Specifications;
using SpecificationPattern.Demo.Infrastructure;
using SpecificatR.Infrastructure.Repositories;
using System.Threading.Tasks;

namespace SpecificationPattern.Demo.Host.Domains
{
    public class CharacterDomain : ICharacterDomain
    {
        private readonly IReadRepository<Character, int> _characterRepository;

        public CharacterDomain(IReadRepository<Character, int> characterRepository)
        {
            _characterRepository = characterRepository;
        }

        public async Task<Character[]> GetAllCharacters()
            => await _characterRepository.GetAllAsync();

        public async Task<Character[]> GetAllCharactersOfTypeHuman()
            => await _characterRepository.GetAllAsync(new CharactersOfTypeHumanSpecification());

        public async Task<Character[]> GetAllCharactersWithAllIncluded()
            => await _characterRepository.GetAllAsync(new CharactersWithAllIncludedSpecification());

        public async Task<Character[]> GetAllCharactersWithFriendsFromTatooine()
                            => await _characterRepository.GetAllAsync(new WithFriendsFromTatooineSpecification());
    }
}
