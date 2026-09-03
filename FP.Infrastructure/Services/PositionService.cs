using FP.Application.Contracts.Repositories;
using FP.Application.Contracts.Services;
using FP.Domain.Entities.Positions;

namespace FP.Infrastructure.Services;

public class PositionService : IPositionService
{
    private readonly IRepository<Position> repository;

    public PositionService(IRepository<Position> repository)
    {
        this.repository = repository;
    }

    public async Task<List<Position>> GetAllAsync()
    {
        return await repository.GetAllAsync();
    }

    public async Task<List<Position>> GetByDepartmentAsync(
        int departmentId)
    {
        return await repository.WhereAsync(
            p => p.DepartmentId == departmentId);
    }

    public async Task<List<Position>> GetDeletedAsync()
    {
        return await repository.GetDeletedAsync();
    }

    public async Task<Position?> GetByIdAsync(int id)
    {
        return await repository.GetByIdAsync(id);
    }
    public async Task<Position?> GetByNameAsync(
    int departmentId,
    string name)
    {
        var positions = await repository.WhereAsync(
            p => p.DepartmentId == departmentId &&
                 p.Name == name);

        return positions.FirstOrDefault();
    }

    public async Task<Position?> GetDeletedByNameAsync(
        int departmentId,
        string name)
    {
        var deletedPositions = await repository.GetDeletedAsync();

        return deletedPositions.FirstOrDefault(
            p => p.DepartmentId == departmentId &&
                 p.Name == name);
    }
}