using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using SpecificationPattern.Demo.Host.Domains;
using System.Threading.Tasks;

namespace SpecificationPattern.Demo.Host.Pages
{
    public class IndexModel : PageModel
    {
        public IndexModel(
            ICharacterDomain characterDomain,
            IEpisodeDomain episodeDomain)
        {
            _characterDomain = characterDomain;
            _episodeDomain = episodeDomain;
        }

        private readonly ICharacterDomain _characterDomain;
        private readonly IEpisodeDomain _episodeDomain;

        [BindProperty]
        public string Result { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostGetAllCharactersNoneIncludes()
        {
            Result = ConvertToJson(await _characterDomain.GetAllCharacters());

            return Page();
        }

        public async Task<IActionResult> OnPostGetAllCharactersWithAllIncluded()
        {
            Result = ConvertToJson(await _characterDomain.GetAllCharactersWithAllIncluded());

            return Page();
        }

        public async Task<IActionResult> OnPostGetCharactersWithFriendsFromTatooine()
        {
            Result = ConvertToJson(await _characterDomain.GetAllCharactersWithFriendsFromTatooine());

            return Page();
        }

        public async Task<IActionResult> OnPostGetAllCharactersOfTypeHuman()
        {
            Result = ConvertToJson(await _characterDomain.GetAllCharactersOfTypeHuman());

            return Page();
        }

        public async Task<IActionResult> OnPostGetAllEpisodes()
        {
            Result = ConvertToJson(await _episodeDomain.GetAllEpisodes());

            return Page();
        }

        public async Task<IActionResult> OnPostGetAllEpisodesOrderedOnTitle()
        {
            Result = ConvertToJson(await _episodeDomain.GetAllEpisodesOrderedOnTitle());

            return Page();
        }

        public async Task<IActionResult> OnPostGetFirstPagedEpisodes()
        {
            Result = ConvertToJson(await _episodeDomain.GetPagedEpisodes(1, 3));

            return Page();
        }
        
        public async Task<IActionResult> OnPostGetSecondPagedEpisodes()
        {
            Result = ConvertToJson(await _episodeDomain.GetPagedEpisodes(2, 3));

            return Page();
        }

        public async Task<IActionResult> OnPostGetThirdPagedEpisodes()
        {
            Result = ConvertToJson(await _episodeDomain.GetPagedEpisodes(3, 3));

            return Page();
        }

        private static string ConvertToJson(object formatObject)
        {
            return JsonConvert.SerializeObject(formatObject, Formatting.Indented, new JsonSerializerSettings() { MaxDepth = 5, ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
        }
    }
}
