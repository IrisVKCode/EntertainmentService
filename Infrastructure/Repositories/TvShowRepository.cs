using Application.IRepositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class TvShowRepository : ITvShowRepository
    {
        private readonly ApplicationDbContext _context;

        public TvShowRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TvShow>> GetAll()
        {
            return await _context.TvShows
                .OrderByDescending(t => t.Premiered)
                .ToListAsync();
        }

        public async Task<TvShow> GetByName(string name)
        {
            return await _context.TvShows
                .Where(show => show.Name.ToLower() == name.ToLower())
                .FirstOrDefaultAsync();
        }

        public async Task<TvShow> GetLatestAdded()
        {
            return await _context.TvShows
                .OrderByDescending(show => show.Id)
                .FirstOrDefaultAsync();
        }

        public async Task Add(TvShow show)
        {
            await _context.TvShows.AddAsync(show);
            await _context.SaveChangesAsync();
        }

        public async Task AddBatch(IEnumerable<TvShow> shows)
        {
            await _context.TvShows.AddRangeAsync(shows);
            await _context.SaveChangesAsync();
        }

        public async Task Update(int id)
        {
            var show = await _context.TvShows.FindAsync(id);

            if (show != null)
            {
                _context.TvShows.Remove(show);
            }

            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var show = await _context.TvShows.FindAsync(id);

            if (show != null)
            {
                _context.TvShows.Remove(show);
            }

            await _context.SaveChangesAsync();
        }
    }
}
