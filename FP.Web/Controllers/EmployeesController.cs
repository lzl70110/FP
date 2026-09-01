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

    public EmployeesController(
        IEmployeeService service,
        ICrudService<Employee> crudService)
    {
        this.service = service;
        this.crudService = crudService;
    }

    public async Task<IActionResult> Index()
    {
        var employees = await service.GetAllAsync();

        return View(employees);
    }

    public async Task<IActionResult> Details(int id)
    {
        var employee = await crudService.ExecuteAsync(
            CrudCommand.Read,
            id);

        if (employee == null)
        {
            return NotFound();
        }

        return View(employee);
    }

    public async Task<IActionResult> Deleted()
    {
        var employees = await service.GetDeletedAsync();

        return View(employees);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Employee employee)
    {
        if (!ModelState.IsValid)
        {
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

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var employee = await service.GetByIdAsync(id);

        if (employee == null)
        {
            return NotFound();
        }

        return View(employee);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Employee employee)
    {
        if (!ModelState.IsValid)
        {
            return View(employee);
        }

        var result = await crudService.ExecuteAsync(
            CrudCommand.Update,
            employee.Id,
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

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
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
        else
        {
            TempData.SetCrudResult(
                new CrudResultViewModel
                {
                    Type = CrudResultType.Warning,
                    Title = "Внимание",
                    Message =
                        "Служителят не беше намерен и не беше изтрит."
                });
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Undelete(int id)
    {
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

        return RedirectToAction(nameof(Deleted));
    }
}