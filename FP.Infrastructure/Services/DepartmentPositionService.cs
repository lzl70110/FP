
using FP.Application.Contracts.Repositories;
using FP.Application.Contracts.Services;
using FP.Domain.Entities.DepartmentPositions;

namespace FP.Infrastructure.Services;

public class DepartmentPositionService
    : IDepartmentPositionService
{
    private readonly IDepartmentPositionRepository repository;

    public DepartmentPositionService(
        IDepartmentPositionRepository repository)
    {
        this.repository = repository;
    }

    public async Task<List<DepartmentPosition>> GetByDepartmentAsync(
        int departmentId)
    {
        return await repository.GetByDepartmentAsync(departmentId);
    }

    public async Task<bool> ExistsAsync(
        int departmentId,
        int positionId)
    {
        return await repository.GetAsync(
            departmentId,
            positionId) != null;
    }

    public async Task<DepartmentPosition?> AddAsync(
        int departmentId,
        int positionId)
    {
        // Check for an existing active relation.
        var existing = await repository.GetAsync(
            departmentId,
            positionId);

        if (existing != null)
        {
            return null;
        }

        // Check for a previously deleted relation.
        var deleted = await repository.GetDeletedAsync(
            departmentId,
            positionId);

        if (deleted != null)
        {
            deleted.IsDeleted = false;
            deleted.DeletedAt = null;
            deleted.DeletedById = null;

            await repository.SaveChangesAsync();

            return deleted;
        }

        // Create a new relation.
        var departmentPosition = new DepartmentPosition
        {
            DepartmentId = departmentId,
            PositionId = positionId
        };

        await repository.AddAsync(departmentPosition);
        await repository.SaveChangesAsync();

        return departmentPosition;
    }

    public async Task<bool> RemoveAsync(
        int departmentId,
        int positionId)
    {
        var departmentPosition = await repository.GetAsync(
            departmentId,
            positionId);

        if (departmentPosition == null)
        {
            return false;
        }

        repository.Delete(departmentPosition);
        await repository.SaveChangesAsync();

        return true;
    }
}
 
