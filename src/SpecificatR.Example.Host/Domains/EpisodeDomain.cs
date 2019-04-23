using SpecificationPattern.Demo.CrossCutting.Entities;
using SpecificationPattern.Demo.Host.Specifications;
using SpecificationPattern.Demo.Infrastructure;
using SpecificatR.Infrastructure.Repositories;
using System.Threading.Tasks;

namespace SpecificationPattern.Demo.Host.Domains
{
    public class EpisodeDomain : IEpisodeDomain
    {
        private readonly IReadRepository<Episode, int> _episodeRepository;

        public EpisodeDomain(IReadRepository<Episode, int> episodeRepository)
        {
            _episodeRepository = episodeRepository;
        }

        public Task<Episode[]> GetAllEpisodes()
            => _episodeRepository.GetAllAsync();

        public Task<Episode[]> GetAllEpisodesOrderedOnTitle()
            => _episodeRepository.GetAllAsync(new OrderEpisodesOnNameSpecification());

        public Task<Episode[]> GetPagedEpisodes(int pageIndex, int pageSize)
            => _episodeRepository.GetAllAsync(new PagedEpisodesSpecification(pageIndex, pageSize));
    }
}
