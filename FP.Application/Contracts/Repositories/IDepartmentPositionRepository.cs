 
using FP.Domain.Entities.DepartmentPositions;

namespace FP.Application.Contracts.Repositories;

public interface IDepartmentPositionRepository
{
    Task<List<DepartmentPosition>> GetByDepartmentAsync(
        int departmentId);

    Task<DepartmentPosition?> GetAsync(
        int departmentId,
        int positionId);

    Task<DepartmentPosition?> GetDeletedAsync(
        int departmentId,
        int positionId);

    Task AddAsync(
        DepartmentPosition departmentPosition);

    void Delete(
        DepartmentPosition departmentPosition);

    Task SaveChangesAsync();
}
 
