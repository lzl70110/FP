using FP.Application.Contracts.Services;
using FP.Domain.Entities.Departments;
using Microsoft.AspNetCore.Mvc;

namespace FP.Web.Controllers;

public class DepartmentsController : Controller
{
    private readonly IDepartmentService service;

    public DepartmentsController(IDepartmentService service)
    {
        this.service = service;
    }

    public async Task<IActionResult> Index()
    {
        var departments = await service.GetAllAsync();

        return View(departments);
    }

    public async Task<IActionResult> Details(int id)
    {
        var department = await service.GetByIdAsync(id);

        if (department == null)
        {
            return NotFound();
        }

        return View(department);
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

        await service.CreateAsync(department);

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

        await service.UpdateAsync(department);

        return RedirectToAction(nameof(Index));
    }
}