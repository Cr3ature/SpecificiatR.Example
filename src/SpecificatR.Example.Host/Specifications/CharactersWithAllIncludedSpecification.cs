using SpecificationPattern.Demo.CrossCutting.Entities;
using SpecificatR.Infrastructure.Abstractions;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SpecificationPattern.Demo.Host.Specifications
{
    public class CharactersWithAllIncludedSpecification : BaseSpecification<Character>
    {
        public CharactersWithAllIncludedSpecification()
            : base(BuildCriteria())
        {
            AddInclude(i => ((Human)i).HomePlanet);
            AddInclude(i => i.CharacterEpisodes.Select(s => s.Episode));
            AddInclude(i => i.CharacterFriends.Select(s => s.Character));
            AddInclude(i => i.FriendCharacters.Select(s => s.Friend));
        }

        private static Expression<Func<Character, bool>> BuildCriteria() => null;
    }
}
