 
using FP.Application.Contracts.Repositories;
using FP.Application.Contracts.Services;
using FP.Domain.Entities.Departments;
using FP.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FP.Infrastructure.Services;

public class DepartmentService(
    IRepository<Department> repository,
    AppDbContext context) : IDepartmentService
{
    private readonly IRepository<Department> repository = repository;
    private readonly AppDbContext context = context;

    public async Task<List<Department>> GetAllAsync()
    {
        return await repository.GetAllAsync();
    }

    public async Task<List<Department>> GetDeletedAsync()
    {
        return await repository.GetDeletedAsync();
    }

    public async Task<Department?> GetByIdAsync(int id)
    {
        return await repository.GetByIdAsync(id);
    }

    public async Task<Department?> GetDetailsAsync(int id)
    {
        return await context.Departments
            .Include(d => d.DepartmentPositions)
                .ThenInclude(dp => dp.Position)
            .FirstOrDefaultAsync(d => d.Id == id);
    }
}
 
