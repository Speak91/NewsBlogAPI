using Microsoft.EntityFrameworkCore;
using NewsBlogAPI.Data.Interfaces;
using NewsBlogAPI.Data.Services;

namespace NewsBlogAPI.Data.Repository
{
    public class NewsRepository : INewsRepository
    {
        private readonly DataContext _context;
        public NewsRepository(DataContext context)
        {
            _context = context;
        }

        public void Create(News item)
        {
            var creationDate = DateTime.UtcNow.Date;
            item.Created_at = creationDate;
            _context.News.Add(item);
        }

        public async void Delete(News news)
        {
            _context.News.Remove(news);
        }

        public async Task<IList<News>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.News.ToListAsync();
        }

        public async Task<News> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.News.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        }

        public async Task<IList<News>> GetByTimePeriodListAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
        {
           return await _context.News.Where(d => d.Created_at >= startDate && d.Created_at <= endDate).ToListAsync(cancellationToken);

        }

        public void Update(News item)
        {
            var creationDate = DateTime.UtcNow.Date;
            item.Created_at = creationDate;
            _context.News.Update(item);
        }
    }
}
