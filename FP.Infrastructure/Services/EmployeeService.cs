 
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
        return await repository.GetAllAsync(
            employee => employee.Position);
    }

    public async Task<List<Employee>> GetDeletedAsync()
    {
        return await repository.GetDeletedAsync(
            employee => employee.Position);
    }

    public async Task<Employee?> GetByIdAsync(int id)
    {
        return await repository.GetByIdAsync(
            id,
            employee => employee.Position);
    }
}
 
