using System.Linq.Expressions;
using FP.Application.Contracts.Identity;
using FP.Domain.Entities.Positions;

namespace FP.Infrastructure.Identity;

public class PositionIdentity : IEntityIdentity<Position>
{
    public Expression<Func<Position, bool>> BuildMatchPredicate(
        Position entity)
    {
        return position =>
            position.DepartmentId == entity.DepartmentId &&
            position.Name == entity.Name;
    }
}