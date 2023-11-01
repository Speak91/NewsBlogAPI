using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsBlogAPI.Data.Interfaces
{
    public interface INewsRepository : IRepository<News>
    {
        /// <summary>
        /// Получение данных за период
        /// </summary>
        /// <param name="timeFrom">Время начала периода</param>
        /// <param name="timeTo">Время окончания периода</param>
        /// <returns></returns>
        Task<IList<News>> GetByTimePeriodListAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);
    }
}
