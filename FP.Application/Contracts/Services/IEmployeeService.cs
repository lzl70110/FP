using FP.Domain.Entities.Employees;

namespace FP.Application.Contracts.Services;

public interface IEmployeeService
{
    Task<List<Employee>> GetAllAsync();

    Task<List<Employee>> GetDeletedAsync();

    Task<Employee?> GetByIdAsync(int id);

    Task CreateAsync(Employee employee);

    Task UpdateAsync(Employee employee);

    Task DeleteAsync(int id);

    Task UndeleteAsync(int id);
}