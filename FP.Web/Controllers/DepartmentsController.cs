using FP.Application.Contracts.Repositories;
using FP.Domain.Entities.Departments;
using Microsoft.AspNetCore.Mvc;

namespace FP.web.Controllers;
public class DepartmentsController : Controller
{
    private readonly IRepository<Department> repository;

    public DepartmentsController(IRepository<Department> repository)
    {
        this.repository = repository;
    }

    public async Task<IActionResult> Index()
    {
        var departments = await repository.GetAllAsync();

        return View(departments);
    }
}