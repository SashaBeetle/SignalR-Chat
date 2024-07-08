using SignalRChat_backend.Data.Entities;

namespace SignalRChat_backend.Data.Interfaces 
{
    public interface IDbEntityService<T> : IDisposable where T : DbItem
    {
        Task<T?> GetById(int id);
        T GetByIdforUser(long id);

        Task<T> Create(T entity);

        Task<T> Update(T entity);

        Task Delete(T entity);
        IQueryable<T> GetAll();
    }
}
