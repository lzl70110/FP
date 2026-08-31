using FP.Domain.Entities.DepartmentPositions;

namespace FP.Application.Contracts.Services;

public interface IDepartmentPositionService
{
    Task<List<DepartmentPosition>> GetByDepartmentAsync(
        int departmentId);

    Task<bool> ExistsAsync(
        int departmentId,
        int positionId);

    Task<DepartmentPosition?> AddAsync(
        int departmentId,
        int positionId);

    Task<bool> RemoveAsync(
        int departmentId,
        int positionId);
}