using SpecificationPattern.Demo.CrossCutting.Entities;
using System.Threading.Tasks;

namespace SpecificationPattern.Demo.Host.Domains
{
    public interface ICharacterDomain
    {
        Task<Character[]> GetAllCharacters();

        Task<Character[]> GetAllCharactersWithAllIncluded();

        Task<Character[]> GetAllCharactersWithFriendsFromTatooine();

        Task<Character[]> GetAllCharactersOfTypeHuman();
    }
}
