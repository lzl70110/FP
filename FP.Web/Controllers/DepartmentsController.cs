using FP.Application.Common;
using FP.Application.Contracts.Services;
using FP.Domain.Entities.Departments;
using FP.Web.Extensions;
using FP.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace FP.Web.Controllers;

public class DepartmentsController(
    IDepartmentService service,
    ICrudService<Department> crudService) : Controller
{
    private readonly IDepartmentService service = service;
    private readonly ICrudService<Department> crudService = crudService;

    public async Task<IActionResult> Index()
    {
        var departments = await service.GetAllAsync();

        return View(departments);
    }

    public async Task<IActionResult> Details(int id)
    {
        var department = await service.GetDetailsAsync(id);

        if (department == null)
        {
            return NotFound();
        }

        return View(department);
    }

    public async Task<IActionResult> Deleted()
    {
        var departments = await service.GetDeletedAsync();

        return View(departments);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Department department)
    {
        if (!ModelState.IsValid)
        {
            return View(department);
        }

        var result = await crudService.ExecuteAsync(
            CrudCommand.Create,
            properties:
            [
                new CrudProperty
                {
                    Name = nameof(Department.Name),
                    Value = department.Name
                },
                new CrudProperty
                {
                    Name = nameof(Department.Notes),
                    Value = department.Notes
                },
                new CrudProperty
                {
                    Name = nameof(Department.IsActive),
                    Value = department.IsActive
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
                        $"Звеното „{result.Name}“ беше създадено успешно."
                });
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var department = await service.GetByIdAsync(id);

        if (department == null)
        {
            return NotFound();
        }

        return View(department);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Department department)
    {
        if (!ModelState.IsValid)
        {
            return View(department);
        }

        var result = await crudService.ExecuteAsync(
            CrudCommand.Update,
            department.Id,
            [
                new CrudProperty
                {
                    Name = nameof(Department.Name),
                    Value = department.Name
                },
                new CrudProperty
                {
                    Name = nameof(Department.Notes),
                    Value = department.Notes
                },
                new CrudProperty
                {
                    Name = nameof(Department.IsActive),
                    Value = department.IsActive
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
                        $"Звеното „{result.Name}“ беше променено успешно."
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
                        "Звеното не беше намерено и не беше променено."
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
                        $"Звеното „{result.Name}“ беше изтрито успешно."
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
                        "Звеното не беше намерено и не беше изтрито."
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
                        $"Звеното „{result.Name}“ беше възстановено успешно."
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
                        "Звеното не беше намерено и не беше възстановено."
                });
        }

        return RedirectToAction(nameof(Deleted));
    }
}