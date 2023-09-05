using Domain;

namespace Application.Services
{
    public interface ITvShowService
    {
        Task AddNewShowsFromApiAsync();
        Task<TvShow> GetByNameAsync(string name);
        Task<IEnumerable<TvShow>> GetAllAsync();
    }
}