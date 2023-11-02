using Microsoft.EntityFrameworkCore;
using NewsBlogAPI.Data.Repository.Interfaces;
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
            return await _context.News.OrderByDescending(d => d.Created_at).ToListAsync();
        }

        public async Task<News> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.News.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        }

        public async Task<IList<News>> GetByTimePeriodListAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
        {
            DateTime utcStartDate = startDate.ToUniversalTime();
            DateTime utcEndDate = endDate.ToUniversalTime();
            return await _context.News
                .Where(d => d.Created_at >= utcStartDate && d.Created_at <= utcEndDate)
                .OrderBy(d => d.Created_at)
                .ToListAsync(cancellationToken);

        }

        public void Update(News item)
        {
            var creationDate = DateTime.UtcNow.Date;
            item.Created_at = creationDate;
            _context.News.Update(item);
        }
    }
}
