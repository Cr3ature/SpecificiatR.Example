using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SpecificationPattern.Demo.CrossCutting.Entities;

namespace SpecificationPattern.Demo.Infrastructure
{
    public class ApplicationContext : DbContext
    {
        public readonly ILogger<ApplicationContext> _logger;

        public ApplicationContext(
            DbContextOptions<ApplicationContext> options,
            ILogger<ApplicationContext> logger)
            : base(options)
        {
            _logger = logger;
            //Database.EnsureCreated();
            //Database.Ensure();
        }

        public virtual DbSet<CharacterFriend> CharacterFriends { get; set; }

        public virtual DbSet<Character> Characters { get; set; }

        public virtual DbSet<CharacterEpisode> CharactersEpisodes { get; set; }

        public virtual DbSet<Droid> Droids { get; set; }

        public virtual DbSet<Episode> Episodes { get; set; }

        public virtual DbSet<Human> Humans { get; set; }

        public virtual DbSet<Planet> Planets { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // episodes
            builder.Entity<Episode>().HasKey(c => c.Id);
            builder.Entity<Episode>().Property(e => e.Id).ValueGeneratedNever();

            // planets
            builder.Entity<Planet>().HasKey(c => c.Id);
            builder.Entity<Planet>().Property(e => e.Id).ValueGeneratedNever();

            // characters
            builder.Entity<Character>().HasKey(c => c.Id);
            builder.Entity<Character>().Property(e => e.Id).ValueGeneratedNever();

            // characters-friends
            builder.Entity<CharacterFriend>().HasKey(t => new { t.CharacterId, t.FriendId });

            builder.Entity<CharacterFriend>()
               .HasOne(cf => cf.Character)
               .WithMany(c => c.CharacterFriends)
               .HasForeignKey(cf => cf.CharacterId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<CharacterFriend>()
                .HasOne(cf => cf.Friend)
                .WithMany(t => t.FriendCharacters)
                .HasForeignKey(cf => cf.FriendId)
                .OnDelete(DeleteBehavior.Restrict);

            // characters - episodes
            builder.Entity<CharacterEpisode>().HasKey(t => new { t.CharacterId, t.EpisodeId });

            builder.Entity<CharacterEpisode>()
                .HasOne(cf => cf.Character)
                .WithMany(c => c.CharacterEpisodes)
                .HasForeignKey(cf => cf.CharacterId)
                .OnDelete(DeleteBehavior.Restrict);

            // humans
            builder.Entity<Human>().HasOne(h => h.HomePlanet).WithMany(p => p.Humans);
        }
    }
}
