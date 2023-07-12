using CRUD.DOMAIN.Models;

namespace CRUD.DOMAIN.Interfaces
{
    public interface IRepository<T> : IDisposable where T : Entity
    {
        Task<List<T>> GetAll();
        Task<T> GetById(Guid Id);
        Task<T> Create(T entity);
        Task<T> Update(T entity);
        Task<T> Remove(Guid Id);
    }
}
