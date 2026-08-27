using FP.Application.Common;
using FP.Application.Contracts.Services;
using FP.Domain.Entities.Departments;
using Microsoft.AspNetCore.Mvc;

namespace FP.Web.Controllers;

public class DepartmentsController : Controller
{
    private readonly IDepartmentService service;
    private readonly ICrudService<Department> crudService;

    public DepartmentsController(
        IDepartmentService service,
        ICrudService<Department> crudService)
    {
        this.service = service;
        this.crudService = crudService;
    }

    public async Task<IActionResult> Index()
    {
        var departments = await service.GetAllAsync();

        return View(departments);
    }

    public async Task<IActionResult> Details(int id)
    {
        var department = await crudService.ExecuteAsync(
            CrudCommand.Read,
            id);

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

        await crudService.ExecuteAsync(
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

        await crudService.ExecuteAsync(
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

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        await service.DeleteAsync(id);

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Undelete(int id)
    {
        await service.UndeleteAsync(id);

        return RedirectToAction(nameof(Deleted));
    }
}