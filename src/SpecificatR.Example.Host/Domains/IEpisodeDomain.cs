using SpecificationPattern.Demo.CrossCutting.Entities;
using System.Threading.Tasks;

namespace SpecificationPattern.Demo.Host.Domains
{
    public interface IEpisodeDomain
    {
        Task<Episode[]> GetAllEpisodes();

        Task<Episode[]> GetAllEpisodesOrderedOnTitle();

        Task<Episode[]> GetPagedEpisodes(int pageIndex, int pageSize);
    }
}
