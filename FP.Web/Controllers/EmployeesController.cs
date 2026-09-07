 using FP.Application.Common;
using FP.Application.Contracts.Services;
using FP.Domain.Entities.Employees;
using FP.Web.Extensions;
using FP.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace FP.Web.Controllers;

public class EmployeesController : Controller
{
    private readonly IEmployeeService service;
    private readonly ICrudService<Employee> crudService;
    private readonly IPositionService positionService;
    private readonly IDepartmentService departmentService;

    public EmployeesController(
        IEmployeeService service,
        ICrudService<Employee> crudService,
        IPositionService positionService,
        IDepartmentService departmentService)
    {
        this.service = service;
        this.crudService = crudService;
        this.positionService = positionService;
        this.departmentService = departmentService;
    }

    public async Task<IActionResult> Index(int departmentId)
    {
        var department = await departmentService.GetByIdAsync(
            departmentId);

        if (department == null)
        {
            return NotFound();
        }

        var employees = (await service.GetAllAsync())
            .Where(employee =>
                employee.Position != null &&
                employee.Position.DepartmentId == departmentId)
            .OrderBy(employee => employee.WorkNumber)
            .ToList();

        ViewData["DepartmentId"] = departmentId;
        ViewData["DepartmentName"] = department.Name;

        return View(employees);
    }

    public async Task<IActionResult> Details(
        int id,
        int departmentId)
    {
        var employee = await service.GetByIdAsync(id);

        if (employee == null ||
            employee.Position == null ||
            employee.Position.DepartmentId != departmentId)
        {
            return NotFound();
        }

        var department = await departmentService.GetByIdAsync(
            departmentId);

        if (department == null)
        {
            return NotFound();
        }

        ViewData["DepartmentId"] = departmentId;
        ViewData["DepartmentName"] = department.Name;

        return View(employee);
    }

    public async Task<IActionResult> Deleted(int departmentId)
    {
        var department = await departmentService.GetByIdAsync(
            departmentId);

        if (department == null)
        {
            return NotFound();
        }

        var employees = (await service.GetDeletedAsync())
            .Where(employee =>
                employee.Position != null &&
                employee.Position.DepartmentId == departmentId)
            .OrderBy(employee => employee.WorkNumber)
            .ToList();

        ViewData["DepartmentId"] = departmentId;
        ViewData["DepartmentName"] = department.Name;

        return View(employees);
    }

    [HttpGet]
    public async Task<IActionResult> Create(int departmentId)
    {
        var department = await departmentService.GetByIdAsync(
            departmentId);

        if (department == null)
        {
            return NotFound();
        }

        await LoadPositionsAsync(departmentId);

        ViewData["DepartmentId"] = departmentId;
        ViewData["DepartmentName"] = department.Name;

        return View(new Employee());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        int departmentId,
        Employee employee)
    {
        var department = await departmentService.GetByIdAsync(
            departmentId);

        if (department == null)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            await LoadPositionsAsync(departmentId);

            ViewData["DepartmentId"] = departmentId;
            ViewData["DepartmentName"] = department.Name;

            return View(employee);
        }

        var position = await positionService.GetByIdAsync(
            employee.PositionId);

        if (position == null ||
            position.DepartmentId != departmentId)
        {
            ModelState.AddModelError(
                nameof(employee.PositionId),
                "Избраната длъжност не принадлежи към това звено.");

            await LoadPositionsAsync(departmentId);

            ViewData["DepartmentId"] = departmentId;
            ViewData["DepartmentName"] = department.Name;

            return View(employee);
        }

        var result = await crudService.ExecuteAsync(
            CrudCommand.Create,
            properties:
            [
                new CrudProperty
                {
                    Name = nameof(Employee.WorkNumber),
                    Value = employee.WorkNumber
                },
                new CrudProperty
                {
                    Name = nameof(Employee.FirstName),
                    Value = employee.FirstName
                },
                new CrudProperty
                {
                    Name = nameof(Employee.MiddleName),
                    Value = employee.MiddleName
                },
                new CrudProperty
                {
                    Name = nameof(Employee.LastName),
                    Value = employee.LastName
                },
                new CrudProperty
                {
                    Name = nameof(Employee.PositionId),
                    Value = employee.PositionId
                },
                new CrudProperty
                {
                    Name = nameof(Employee.Notes),
                    Value = employee.Notes
                },
                new CrudProperty
                {
                    Name = nameof(Employee.IsActive),
                    Value = employee.IsActive
                }
            ]);

        if (result != null)
        {
            TempData.SetCrudResult(
                new CrudResultViewModel
                {
                    Type = CrudResultType.Success,
                    Title = "Успешно",
                    Message =
                        $"Служителят „{result.FirstName} {result.LastName}“ беше създаден успешно."
                });
        }

        return RedirectToAction(
            nameof(Index),
            new { departmentId });
    }

    [HttpGet]
    public async Task<IActionResult> Edit(
        int id,
        int departmentId)
    {
        var employee = await service.GetByIdAsync(id);

        if (employee == null ||
            employee.Position == null ||
            employee.Position.DepartmentId != departmentId)
        {
            return NotFound();
        }

        var department = await departmentService.GetByIdAsync(
            departmentId);

        if (department == null)
        {
            return NotFound();
        }

        await LoadPositionsAsync(departmentId);

        ViewData["DepartmentId"] = departmentId;
        ViewData["DepartmentName"] = department.Name;

        return View(employee);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        int id,
        int departmentId,
        Employee employee)
    {
        var department = await departmentService.GetByIdAsync(
            departmentId);

        if (department == null)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            await LoadPositionsAsync(departmentId);

            ViewData["DepartmentId"] = departmentId;
            ViewData["DepartmentName"] = department.Name;

            return View(employee);
        }

        var existingEmployee = await service.GetByIdAsync(id);

        if (existingEmployee == null ||
            existingEmployee.Position == null ||
            existingEmployee.Position.DepartmentId != departmentId)
        {
            return NotFound();
        }

        var position = await positionService.GetByIdAsync(
            employee.PositionId);

        if (position == null ||
            position.DepartmentId != departmentId)
        {
            ModelState.AddModelError(
                nameof(employee.PositionId),
                "Избраната длъжност не принадлежи към това звено.");

            await LoadPositionsAsync(departmentId);

            ViewData["DepartmentId"] = departmentId;
            ViewData["DepartmentName"] = department.Name;

            return View(employee);
        }

        var result = await crudService.ExecuteAsync(
            CrudCommand.Update,
            id,
            [
                new CrudProperty
                {
                    Name = nameof(Employee.FirstName),
                    Value = employee.FirstName
                },
                new CrudProperty
                {
                    Name = nameof(Employee.MiddleName),
                    Value = employee.MiddleName
                },
                new CrudProperty
                {
                    Name = nameof(Employee.LastName),
                    Value = employee.LastName
                },
                new CrudProperty
                {
                    Name = nameof(Employee.PositionId),
                    Value = employee.PositionId
                },
                new CrudProperty
                {
                    Name = nameof(Employee.Notes),
                    Value = employee.Notes
                },
                new CrudProperty
                {
                    Name = nameof(Employee.IsActive),
                    Value = employee.IsActive
                }
            ]);

        if (result != null)
        {
            TempData.SetCrudResult(
                new CrudResultViewModel
                {
                    Type = CrudResultType.Success,
                    Title = "Успешно",
                    Message =
                        $"Служителят „{result.FirstName} {result.LastName}“ беше променен успешно."
                });
        }
        else
        {
            TempData.SetCrudResult(
                new CrudResultViewModel
                {
                    Type = CrudResultType.Warning,
                    Title = "Внимание",
                    Message =
                        "Служителят не беше намерен и не беше променен."
                });
        }

        return RedirectToAction(
            nameof(Index),
            new { departmentId });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(
        int id,
        int departmentId)
    {
        var employee = await service.GetByIdAsync(id);

        if (employee == null ||
            employee.Position == null ||
            employee.Position.DepartmentId != departmentId)
        {
            TempData.SetCrudResult(
                new CrudResultViewModel
                {
                    Type = CrudResultType.Warning,
                    Title = "Внимание",
                    Message =
                        "Служителят не беше намерен и не беше изтрит."
                });

            return RedirectToAction(
                nameof(Index),
                new { departmentId });
        }

        var result = await crudService.ExecuteAsync(
            CrudCommand.Delete,
            id);

        if (result != null)
        {
            TempData.SetCrudResult(
                new CrudResultViewModel
                {
                    Type = CrudResultType.Success,
                    Title = "Успешно",
                    Message =
                        $"Служителят „{result.FirstName} {result.LastName}“ беше изтрит успешно."
                });
        }

        return RedirectToAction(
            nameof(Index),
            new { departmentId });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Undelete(
        int id,
        int departmentId)
    {
        var deletedEmployee = (await service.GetDeletedAsync())
            .FirstOrDefault(employee =>
                employee.Id == id &&
                employee.Position != null &&
                employee.Position.DepartmentId == departmentId);

        if (deletedEmployee == null)
        {
            TempData.SetCrudResult(
                new CrudResultViewModel
                {
                    Type = CrudResultType.Warning,
                    Title = "Внимание",
                    Message =
                        "Служителят не беше намерен и не беше възстановен."
                });

            return RedirectToAction(
                nameof(Deleted),
                new { departmentId });
        }

        var result = await crudService.ExecuteAsync(
            CrudCommand.Undelete,
            id);

        if (result != null)
        {
            TempData.SetCrudResult(
                new CrudResultViewModel
                {
                    Type = CrudResultType.Success,
                    Title = "Успешно",
                    Message =
                        $"Служителят „{result.FirstName} {result.LastName}“ беше възстановен успешно."
                });
        }
        else
        {
            TempData.SetCrudResult(
                new CrudResultViewModel
                {
                    Type = CrudResultType.Warning,
                    Title = "Внимание",
                    Message =
                        "Служителят не беше намерен и не беше възстановен."
                });
        }

        return RedirectToAction(
            nameof(Deleted),
            new { departmentId });
    }

    private async Task LoadPositionsAsync(int departmentId)
    {
        var positions = await positionService.GetByDepartmentAsync(
            departmentId);

        ViewBag.Positions = positions;
    }
}
 
