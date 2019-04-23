using SpecificationPattern.Demo.CrossCutting.Entities;
using System;
using System.Linq.Expressions;
using System.Linq;
using SpecificatR.Infrastructure.Abstractions;

namespace SpecificationPattern.Demo.Host.Specifications
{
    public class WithFriendsFromTatooineSpecification : BaseSpecification<Character>
    {
        public WithFriendsFromTatooineSpecification()
            : base(BuildCriteria())
        {
        }

        private static Expression<Func<Character, bool>> BuildCriteria()
            => (i => 
            i.CharacterFriends != null && 
            i.CharacterFriends.Any(a => 
                a.Friend.GetType().Equals(typeof(Human)) &&
                ((Human)a.Friend).HomePlanet.Name.Equals("Tatooine")
            ));
    }
}
