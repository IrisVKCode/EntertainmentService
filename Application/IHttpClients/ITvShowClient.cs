
using Domain;

namespace Application.IHttpClients
{
    public interface ITvShowClient
    {
        Task<IEnumerable<TvShow>> GetTvShowsAsync(int page);
    }
}
