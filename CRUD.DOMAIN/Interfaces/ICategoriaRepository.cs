using CRUD.DOMAIN.Models;

namespace CRUD.DOMAIN.Interfaces
{
    public interface ICategoriaRepository : IRepository<Categoria>
    {
        Task<IEnumerable<Categoria>> GetAllAndProducts();
    }
}
