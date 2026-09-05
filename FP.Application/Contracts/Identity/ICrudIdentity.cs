using System.Linq.Expressions;
using FP.Domain.Common;

namespace FP.Application.Contracts.Identity;

public interface IEntityIdentity<TEntity>
    where TEntity : SoftDeletableEntity
{
    Expression<Func<TEntity, bool>> BuildMatchPredicate(
        TEntity entity);
}