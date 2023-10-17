using Application.IHttpClients;
using Application.IRepositories;
using Domain;

namespace Application.Services
{
    public class TvShowService : ITvShowService
    {
        private readonly ITvShowRepository _tvShowRepository;
        private readonly ITvShowClient _tvShowClient;

        private static readonly DateTime PremieredAfter = new DateTime(2014, 01, 01);

        public TvShowService(
            ITvShowRepository tvShowRepository,
            ITvShowClient tvShowClient)
        {
            _tvShowRepository = tvShowRepository;
            _tvShowClient = tvShowClient;
        }

        public async Task RetrieveNewestShows()
        {
            var latestAddedShow = await _tvShowRepository.GetLatestAdded();
            int latestId = latestAddedShow?.ExternalId ?? 0;
            var page = (int)Math.Floor((double)latestId / 250);

            IEnumerable<TvShow> shows;
            do
            {
                shows = await _tvShowClient.GetTvShowsAsync(page);
                var showsFiltered = shows.Where(s => s.Premiered != null && s.Premiered > PremieredAfter && s.ExternalId > latestId);
                await _tvShowRepository.AddBatch(showsFiltered);
                page++;
            } 
            while (shows.Any());
        }

        public async Task<TvShow?> GetByName(string name)
        {
            return await _tvShowRepository.GetByName(name);
        }

        public async Task<IEnumerable<TvShow>> GetAll()
        {
            return await _tvShowRepository.GetAll();
        }
    }
}
