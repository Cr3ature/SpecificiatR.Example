using SpecificatR.Infrastructure.Abstractions;
using System.Collections.Generic;

namespace SpecificationPattern.Demo.CrossCutting.Entities
{
    public class Character : IBaseEntity<int>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<CharacterEpisode> CharacterEpisodes { get; set; }

        public ICollection<CharacterFriend> CharacterFriends { get; set; }

        public ICollection<CharacterFriend> FriendCharacters { get; set; }
    }
}
