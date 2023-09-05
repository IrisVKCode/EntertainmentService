﻿
using Application.Helpers;
using Application.IHttpClients;
using Domain;
using Infrastructure.Models;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

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
                var showsMapped = shows.Select(s => new Domain.TvShow(
                    s.Id, s.Name, s.Language, s.Premiered, s.Genres, HtmlHelpers.StripHTML(s.Summary)));
                return showsMapped;
            }
            else
            {
                return null;
            }
        }
    }
}
