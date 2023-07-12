using CRUD.DOMAIN.Interfaces;
using CRUD.DOMAIN.Models;
using CRUD.INFRA.Data;
using Microsoft.EntityFrameworkCore;

namespace CRUD.INFRA.Repositories;

public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
{
    public CategoriaRepository(ApplicationContext context) : base(context) { }

    public async Task<IEnumerable<Categoria>> GetAllAndProducts()
    {
        return await Db.Categorias.AsNoTracking().Include(p => p.Produtos).ToListAsync();
    }
}
