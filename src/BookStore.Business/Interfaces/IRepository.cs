using System.Linq.Expressions;
using BookStore.Business.Models;

namespace BookStore.Business.Interfaces;

public interface IRepository<TEntity> : IDisposable where TEntity : Entity
{
    Task<bool> AddAsync(TEntity entity);
    Task<TEntity?> GetByIdAsync(int id);
    Task<List<TEntity>> GetAllAsync();
    Task<bool> UpdateAsync(TEntity entity);
    Task<bool> RemoveByIdAsync(int id);
    Task<IEnumerable<TEntity>> SearchAsync(Expression<Func<TEntity, bool>> predicate);
    Task<bool> SaveChangesAsync();
}