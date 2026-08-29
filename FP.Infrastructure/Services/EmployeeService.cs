using FP.Application.Contracts.Repositories;
using FP.Application.Contracts.Services;
using FP.Domain.Entities.Employees;

namespace FP.Infrastructure.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IRepository<Employee> repository;

    public EmployeeService(IRepository<Employee> repository)
    {
        this.repository = repository;
    }

    public async Task<List<Employee>> GetAllAsync()
    {
        return await repository.GetAllAsync();
    }

    public async Task<List<Employee>> GetDeletedAsync()
    {
        return await repository.GetDeletedAsync();
    }

    public async Task<Employee?> GetByIdAsync(int id)
    {
        return await repository.GetByIdAsync(id);
    }

    public async Task CreateAsync(Employee employee)
    {
        await repository.AddAsync(employee);
        await repository.SaveChangesAsync();
    }

    public async Task UpdateAsync(Employee employee)
    {
        repository.Update(employee);
        await repository.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var employee = await repository.GetByIdAsync(id);

        if (employee == null)
        {
            return;
        }

        repository.Delete(employee);
        await repository.SaveChangesAsync();
    }

    public async Task UndeleteAsync(int id)
    {
        var employee = await repository.GetDeletedByIdAsync(id);

        if (employee == null)
        {
            return;
        }

        employee.IsDeleted = false;
        employee.DeletedAt = null;
        employee.DeletedById = null;

        repository.Update(employee);
        await repository.SaveChangesAsync();
    }
}