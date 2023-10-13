using Domain;

namespace Application.IRepositories
{
    public interface ITvShowRepository
    {
        Task Add(TvShow show);
        Task AddBatch(IEnumerable<TvShow> shows);
        Task Delete(int id);
        Task<IEnumerable<TvShow>> GetAll();
        Task<TvShow> GetByName(string name);
        TvShow GetLatestAdded();
        Task Update(int id);
    }
}