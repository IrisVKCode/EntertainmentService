using Application.IHttpClients;
using Application.IRepositories;
using Domain;

namespace Application.Services
{
    public class TvShowService : ITvShowService
    {
        private readonly ITvShowRepository _tvShowRepository;
        private readonly ITvShowClient _tvShowClient;

        public TvShowService(
            ITvShowRepository tvShowRepository,
            ITvShowClient tvShowClient)
        {
            _tvShowRepository = tvShowRepository;
            _tvShowClient = tvShowClient;
        }

        public async Task AddNewShowsFromApiAsync()
        {
            var startPage = 0;
            var latestAddedShow = _tvShowRepository.GetLatestAdded();
            if (latestAddedShow?.Id != null)
                startPage = (int)Math.Floor((double)latestAddedShow.Id / 250);

            IEnumerable<TvShow> shows = null;
            do
            {
                shows = await _tvShowClient.GetTvShowsAsync(startPage);
                var showsFiltered = shows.Where(s => s.Premiered > new DateTime(2014, 01, 01));
                await _tvShowRepository.AddBatch(showsFiltered);

            } while (shows != null);
        }

        public async Task<TvShow> GetByNameAsync(string name)
        {
            return await _tvShowRepository.GetByName(name);
        }

        public async Task<IEnumerable<TvShow>> GetAllAsync()
        {
            return await _tvShowRepository.GetAll();
        }
    }
}
