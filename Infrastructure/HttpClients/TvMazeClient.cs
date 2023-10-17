
using Application.Helpers;
using Application.IHttpClients;
using Newtonsoft.Json;

namespace Infrastructure.HttpClients
{
    public class TvMazeClient : ITvShowClient
    {
        private readonly HttpClient _httpClient;

        public TvMazeClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Domain.TvShow>> GetTvShowsAsync(int page)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"shows?page={page}");

            if (response.IsSuccessStatusCode)
            {
                string jsonContent = await response.Content.ReadAsStringAsync();
                var shows = JsonConvert.DeserializeObject<List<Models.TvShow>>(jsonContent);

                if (shows != null)
                {
                    var showsMapped = shows.Select(s => new Domain.TvShow(
                        s.ExternalId, s.Name, s.Language, s.Premiered, s.Genres, HtmlHelpers.StripHTML(s.Summary)));
                    return showsMapped;
                }
            }

            return new List<Domain.TvShow>();
        }
    }
}
