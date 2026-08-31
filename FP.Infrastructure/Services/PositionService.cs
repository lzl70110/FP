 
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

    public async Task<List<Position>> GetDeletedAsync()
    {
        return await repository.GetDeletedAsync();
    }

    public async Task<Position?> GetByIdAsync(int id)
    {
        return await repository.GetByIdAsync(id);
    }
}
 
