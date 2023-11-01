namespace NewsBlogAPI.Data.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IList<T>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        void Create(T item);
        void Update(T item);
        void Delete(T item);
    }
}
