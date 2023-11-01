namespace NewsBlogAPI.Data.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _dataContext;

        public UnitOfWork(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public Task SaveChangesAsync(CancellationToken cancellationToken = default)
        => _dataContext.SaveChangesAsync(cancellationToken);
    }
}
