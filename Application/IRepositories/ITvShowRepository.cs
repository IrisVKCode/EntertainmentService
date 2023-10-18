using Domain;

namespace Application.IRepositories
{
    public interface ITvShowRepository
    {
        Task<IEnumerable<TvShow>> GetAll();
        Task<TvShow?> GetByName(string name);
        Task<TvShow?> GetLatestAdded();
        Task Add(TvShow show);
        Task AddBatch(IEnumerable<TvShow> shows);
        Task Delete(int id);
    }
}