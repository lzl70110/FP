using FP.Application.Common;

namespace FP.Application.Contracts.Services;

public interface ICrudService<TEntity>
{
    Task<TEntity?> ExecuteAsync(
        CrudCommand command,
        int? id = null,
        params CrudProperty[] properties);
}