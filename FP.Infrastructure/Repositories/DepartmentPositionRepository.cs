 
using FP.Application.Contracts.Repositories;
using FP.Domain.Entities.DepartmentPositions;
using FP.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FP.Infrastructure.Repositories;

public class DepartmentPositionRepository
    : IDepartmentPositionRepository
{
    private readonly AppDbContext context;

    public DepartmentPositionRepository(AppDbContext context)
    {
        this.context = context;
    }

    public async Task<List<DepartmentPosition>> GetByDepartmentAsync(
        int departmentId)
    {
        return await context.DepartmentPositions
            .Include(dp => dp.Position)
            .Where(dp => dp.DepartmentId == departmentId)
            .ToListAsync();
    }

    public async Task<DepartmentPosition?> GetAsync(
        int departmentId,
        int positionId)
    {
        return await context.DepartmentPositions
            .FirstOrDefaultAsync(
                dp => dp.DepartmentId == departmentId
                    && dp.PositionId == positionId);
    }

    public async Task<DepartmentPosition?> GetDeletedAsync(
        int departmentId,
        int positionId)
    {
        return await context.DepartmentPositions
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(
                dp => dp.DepartmentId == departmentId
                    && dp.PositionId == positionId
                    && dp.IsDeleted);
    }

    public async Task AddAsync(
        DepartmentPosition departmentPosition)
    {
        await context.DepartmentPositions
            .AddAsync(departmentPosition);
    }

    public void Delete(
        DepartmentPosition departmentPosition)
    {
        // Audit информацията се задава централизирано в AppDbContext.
        departmentPosition.IsDeleted = true;

        context.DepartmentPositions
            .Update(departmentPosition);
    }

    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }
}
 
