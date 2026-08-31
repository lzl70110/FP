 
using FP.Domain.Entities.Departments;

namespace FP.Application.Contracts.Services;

public interface IDepartmentService
{
    Task<List<Department>> GetAllAsync();

    Task<List<Department>> GetDeletedAsync();

    Task<Department?> GetByIdAsync(int id);

    Task<Department?> GetDetailsAsync(int id);
}
 
