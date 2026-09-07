 
using System.Linq.Expressions;
using FP.Domain.Common;

namespace FP.Application.Contracts.Repositories;

public interface IRepository<TEntity>
    where TEntity : SoftDeletableEntity
{
    Task<TEntity?> GetByIdAsync(int id);

    Task<TEntity?> GetByIdAsync(
        int id,
        params Expression<Func<TEntity, object>>[] includes);

    Task<List<TEntity>> GetAllAsync();

    Task<List<TEntity>> GetAllAsync(
        params Expression<Func<TEntity, object>>[] includes);

    Task<List<TEntity>> GetDeletedAsync();

    Task<List<TEntity>> GetDeletedAsync(
        params Expression<Func<TEntity, object>>[] includes);

    Task<List<TEntity>> WhereAsync(
        Expression<Func<TEntity, bool>> predicate);

    Task<TEntity?> FirstOrDefaultAsync(
        Expression<Func<TEntity, bool>> predicate);

    Task<TEntity?> FirstDeletedOrDefaultAsync(
        Expression<Func<TEntity, bool>> predicate);

    Task AddAsync(TEntity entity);

    void Update(TEntity entity);

    void Delete(TEntity entity);

    Task SaveChangesAsync();

    Task<TEntity?> GetDeletedByIdAsync(int id);
}
 