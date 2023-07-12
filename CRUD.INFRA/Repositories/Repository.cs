using CRUD.DOMAIN.Interfaces;
using CRUD.DOMAIN.Models;
using CRUD.INFRA.Data;
using Microsoft.EntityFrameworkCore;

namespace CRUD.INFRA.Repositories;

public abstract class Repository<T> : IRepository<T> where T : Entity, new()
{
    protected readonly ApplicationContext Db;
    protected readonly DbSet<T> DbSet;

    public Repository(ApplicationContext context)
    {
        Db = context;
        DbSet = Db.Set<T>();
    }

    //============================================================================================================================
    public virtual async Task<List<T>> GetAll()
    {
        return await DbSet.AsNoTracking().ToListAsync();
    }

    //============================================================================================================================
    public virtual async Task<T> GetById(Guid Id)
    {
        return await DbSet.FindAsync(Id);
    }

    //============================================================================================================================
    public virtual async Task<T> Create(T entity)
    {
        var result = await DbSet.AddAsync(entity);
        await SaveChanges();
        return result.Entity;
    }

    //============================================================================================================================
    public virtual async Task<T> Update(T entity)
    {
        var result = await DbSet.FirstOrDefaultAsync(p => p.Id == entity.Id);
        if(result is not null)
        {
            DbSet.Update(entity);
            await SaveChanges();
            return result;
        }
        return null;
    }

    //============================================================================================================================
    public virtual async Task<T> Remove(Guid Id)
    {
        var result = await DbSet.FirstOrDefaultAsync(p => p.Id == Id);
        if(result is not null)
        {
            DbSet.Remove(new T { Id = Id });
            await SaveChanges();
        }
        return null;
    }

    private async Task<int> SaveChanges()
    {
        return await Db.SaveChangesAsync();
    }

    public void Dispose()
    {
        Db.Dispose();
    }
}
