using SpecificationPattern.Demo.CrossCutting.Entities;
using SpecificatR.Infrastructure.Abstractions;
using System;
using System.Linq.Expressions;

namespace SpecificationPattern.Demo.Host.Specifications
{
    public class CharactersOfTypeHumanSpecification : BaseSpecification<Character>
    {
        public CharactersOfTypeHumanSpecification()
            : base(BuildCriteria())
        {
        }

        private static Expression<Func<Character, bool>> BuildCriteria()
            => (i => i.GetType().Equals(typeof(Human)));
    }
}
