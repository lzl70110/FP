using FP.Application.Contracts.Repositories;
using FP.Application.Contracts.Services;
using FP.Domain.Entities.Departments;

namespace FP.Infrastructure.Services;

public class DepartmentService : IDepartmentService
{
    private readonly IRepository<Department> repository;

    public DepartmentService(IRepository<Department> repository)
    {
        this.repository = repository;
    }

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

    public async Task CreateAsync(Department department)
    {
        await repository.AddAsync(department);
        await repository.SaveChangesAsync();
    }

    public async Task UpdateAsync(Department department)
    {
        repository.Update(department);
        await repository.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var department = await repository.GetByIdAsync(id);

        if (department == null)
        {
            return;
        }

        repository.Delete(department);
        await repository.SaveChangesAsync();
    }

    public async Task UndeleteAsync(int id)
    {
        var department = await repository.GetDeletedByIdAsync(id);

        if (department == null)
        {
            return;
        }

        department.IsDeleted = false;
        department.DeletedAt = null;
        department.DeletedById = null;

        repository.Update(department);
        await repository.SaveChangesAsync();
    }
}