 
using FP.Domain.Entities.Positions;

namespace FP.Application.Contracts.Services;

public interface IPositionService
{
    Task<List<Position>> GetAllAsync();

    Task<List<Position>> GetDeletedAsync();

    Task<Position?> GetByIdAsync(int id);
}
 