using FP.Domain.Entities.Employees;

namespace FP.Application.Contracts.Services;

public interface IEmployeeService
{
    Task<List<Employee>> GetAllAsync();

    Task<List<Employee>> GetDeletedAsync();

    Task<Employee?> GetByIdAsync(int id);
}