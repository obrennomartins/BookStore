using System.Linq.Expressions;
using BookStore.Business.Interfaces;
using BookStore.Business.Models;
using BookStore.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Data.Repositories;

public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
{
    protected readonly BookStoreContext Db;
    protected readonly DbSet<TEntity> DbSet;

    protected Repository(BookStoreContext db)
    {
        Db = db;
        DbSet = db.Set<TEntity>();
    }

    public void Dispose()
    {
        Db.Dispose();
        GC.SuppressFinalize(this);
    }

    public async Task<bool> AddAsync(TEntity entity)
    {
        DbSet.Add(entity);
        return await SaveChangesAsync();
    }

    public async Task<TEntity?> GetByIdAsync(int id)
    {
        return await DbSet.FindAsync(id);
    }

    public async Task<List<TEntity>> GetAllAsync()
    {
        return await DbSet.ToListAsync();
    }

    public async Task<bool> UpdateAsync(TEntity entity)
    {
        DbSet.Update(entity);
        return await SaveChangesAsync();
    }

    public async Task<bool> RemoveByIdAsync(int id)
    {
        DbSet.Remove(new TEntity {Id = id});
        return await SaveChangesAsync();
    }

    public async Task<IEnumerable<TEntity>> SearchAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await Db.SaveChangesAsync() > 0;
    }
}