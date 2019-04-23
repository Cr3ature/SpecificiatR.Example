using Microsoft.Extensions.Logging;
using SpecificationPattern.Demo.CrossCutting.Entities;
using System.Collections.Generic;
using System.Linq;

namespace SpecificationPattern.Demo.Infrastructure
{
    public static class DataSeed
    {
        public static void EnsureSeedData(this ApplicationContext context)
        {
            context._logger.LogInformation("Seeding database");

            var episode1 = new Episode
            {
                Title = "The Phantom Menace",
                Id = 1
            };
            var episode2 = new Episode
            {
                Title = "Attack of the Clones",
                Id = 2
            };
            var episode3 = new Episode
            {
                Title = "Revenge of the Sith",
                Id = 3
            };
            var episode4 = new Episode
            {
                Title = "A New Hope",
                Id = 4
            };
            var episode5 = new Episode
            {
                Title = "The Empire Strikes Back",
                Id = 5
            };
            var episode6 = new Episode
            {
                Title = "Return of the Jedi",
                Id = 6
            };
            var episode7 = new Episode
            {
                Title = "The Force Awakens",
                Id = 7
            };
            var episode8 = new Episode
            {
                Title = "The Last Jedi",
                Id = 8
            };
            var episodes = new List<Episode>
            {
                episode1,
                episode2,
                episode3,
                episode4,
                episode5,
                episode6,
                episode7,
                episode8
            };
            if (!context.Episodes.Any())
            {
                context._logger.LogInformation("Seeding planets");
                context.Episodes.AddRange(episodes);
                context.SaveChanges();
            }

            // planets
            var tatooine = new Planet
            {
                Id = 1,
                Name = "Tatooine",
                Climate = "Arid",
                Population = "200000",
                Terrain = "desert"
            };
            var alderaan = new Planet
            {
                Id = 2,
                Name = "Alderaan",
                Climate = "Temperate",
                Population = "2000000000",
                Terrain = "grasslands, mountains"
            };
            var planets = new List<Planet>
            {
                tatooine,
                alderaan
            };
            if (!context.Planets.Any())
            {
                context._logger.LogInformation("Seeding planets");
                context.Planets.AddRange(planets);
                context.SaveChanges();
            }

            // humans
            var luke = new Human
            {
                Id = 1000,
                Name = "Luke Skywalker",
                CharacterEpisodes = new List<CharacterEpisode>
                {
                    new CharacterEpisode { Episode = episode4 },
                    new CharacterEpisode { Episode = episode5 },
                    new CharacterEpisode { Episode = episode6 },
                    new CharacterEpisode { Episode = episode7 },
                    new CharacterEpisode { Episode = episode8 }
                },
                HomePlanet = tatooine
            };
            var vader = new Human
            {
                Id = 1001,
                Name = "Darth Vader",
                CharacterEpisodes = new List<CharacterEpisode>
                {
                    new CharacterEpisode { Episode = episode3 },
                    new CharacterEpisode { Episode = episode4 },
                    new CharacterEpisode { Episode = episode5 },
                    new CharacterEpisode { Episode = episode6 },
                },
                HomePlanet = tatooine
            };
            var han = new Human
            {
                Id = 1002,
                Name = "Han Solo",
                CharacterEpisodes = new List<CharacterEpisode>
                {
                    new CharacterEpisode { Episode = episode4 },
                    new CharacterEpisode { Episode = episode5 },
                    new CharacterEpisode { Episode = episode6 },
                    new CharacterEpisode { Episode = episode7 }
                },
                HomePlanet = tatooine
            };
            var leia = new Human
            {
                Id = 1003,
                Name = "Leia Organa",
                CharacterEpisodes = new List<CharacterEpisode>
                {
                    new CharacterEpisode { Episode = episode4 },
                    new CharacterEpisode { Episode = episode5 },
                    new CharacterEpisode { Episode = episode6 },
                    new CharacterEpisode { Episode = episode7 },
                    new CharacterEpisode { Episode = episode8 }
                },
                HomePlanet = alderaan
            };
            var humans = new List<Human>
            {
                luke,
                vader,
                han,
                leia
            };
            if (!context.Humans.Any())
            {
                context._logger.LogInformation("Seeding humans");
                context.Humans.AddRange(humans);
                context.SaveChanges();
            }

            // droids
            var threepio = new Droid
            {
                Id = 2000,
                Name = "C-3PO",
                CharacterEpisodes = new List<CharacterEpisode>
                {
                    new CharacterEpisode { Episode = episode2 },
                    new CharacterEpisode { Episode = episode3 },
                    new CharacterEpisode { Episode = episode4 },
                    new CharacterEpisode { Episode = episode5 },
                    new CharacterEpisode { Episode = episode6 },
                },
                PrimaryFunction = "Protocol"
            };
            var artoo = new Droid
            {
                Id = 2001,
                Name = "R2-D2",
                CharacterEpisodes = new List<CharacterEpisode>
                {
                    new CharacterEpisode { Episode = episode2 },
                    new CharacterEpisode { Episode = episode3 },
                    new CharacterEpisode { Episode = episode4 },
                    new CharacterEpisode { Episode = episode5 },
                    new CharacterEpisode { Episode = episode6 },
                },
                PrimaryFunction = "Astromech"
            };
            var droids = new List<Droid>
            {
                threepio,
                artoo
            };
            if (!context.Droids.Any())
            {
                context._logger.LogInformation("Seeding droids");
                context.Droids.AddRange(droids);
                context.SaveChanges();
            }

            // update character's friends
            luke.CharacterFriends = new List<CharacterFriend>
            {
                new CharacterFriend { Friend = han },
                new CharacterFriend { Friend = leia },
                new CharacterFriend { Friend = threepio },
                new CharacterFriend { Friend = artoo }
            };
            han.CharacterFriends = new List<CharacterFriend>
            {
                new CharacterFriend { Friend = luke },
                new CharacterFriend { Friend = leia },
                new CharacterFriend { Friend = artoo }
            };
            leia.CharacterFriends = new List<CharacterFriend>
            {
                new CharacterFriend { Friend = luke },
                new CharacterFriend { Friend = han },
                new CharacterFriend { Friend = threepio },
                new CharacterFriend { Friend = artoo }
            };
            threepio.CharacterFriends = new List<CharacterFriend>
            {
                new CharacterFriend { Friend = luke },
                new CharacterFriend { Friend = han },
                new CharacterFriend { Friend = leia },
                new CharacterFriend { Friend = artoo }
            };
            artoo.CharacterFriends = new List<CharacterFriend>
            {
                new CharacterFriend { Friend = luke },
                new CharacterFriend { Friend = han },
                new CharacterFriend { Friend = leia }
            };
            var characters = new List<Character>
            {
                luke,
                vader,
                han,
                leia,
                threepio,
                artoo
            };
            if (!context.CharacterFriends.Any())
            {
                context._logger.LogInformation("Seeding character's friends");
                context.Characters.UpdateRange(characters);
                context.SaveChanges();
            }

            // update episode's heroes
            episode3.Hero = artoo;
            episode4.Hero = luke;
            episode5.Hero = artoo;
            context._logger.LogInformation("Seeding episode's heroes");
            context.SaveChanges();
        }
    }
}
