using Domain;

namespace Application.Services
{
    public interface ITvShowService
    {
        Task RetrieveNewestShows();
        Task<TvShow?> GetByName(string name);
        Task<IEnumerable<TvShow>> GetAll();
    }
}