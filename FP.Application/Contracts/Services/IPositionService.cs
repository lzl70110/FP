using FP.Domain.Entities.Positions;

namespace FP.Application.Contracts.Services;

public interface IPositionService
{
    Task<List<Position>> GetAllAsync();

    Task<List<Position>> GetByDepartmentAsync(
        int departmentId);

    Task<List<Position>> GetDeletedAsync();

    Task<Position?> GetByIdAsync(int id);

    Task<Position?> GetDeletedByIdAsync(int id);

    Task<Position?> GetDeletedByNameAsync(
        int departmentId,
        string name);

    Task<Position?> GetByNameAsync(
        int departmentId,
        string name);
}