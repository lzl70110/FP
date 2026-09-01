using FP.Application.Common;
using FP.Application.Contracts.Services;
using FP.Domain.Entities.Positions;
using FP.Web.Extensions;
using FP.Web.Models;
using FP.Web.Models.Forms;
using Microsoft.AspNetCore.Mvc;

namespace FP.Web.Controllers;

public class PositionsController(
    IPositionService service,
    ICrudService<Position> crudService) : Controller
{
    private readonly IPositionService service = service;
    private readonly ICrudService<Position> crudService = crudService;

    public async Task<IActionResult> Index(int departmentId)
    {
        var positions = await service.GetByDepartmentAsync(
            departmentId);

        ViewData["DepartmentId"] = departmentId;

        return View(positions);
    }

    public async Task<IActionResult> Details(
        int id,
        int departmentId)
    {
        var position = await crudService.ExecuteAsync(
            CrudCommand.Read,
            id);

        if (position == null ||
            position.DepartmentId != departmentId)
        {
            return NotFound();
        }

        ViewData["DepartmentId"] = departmentId;

        return View(position);
    }

    public async Task<IActionResult> Deleted()
    {
        var positions = await service.GetDeletedAsync();

        return View(positions);
    }

    [HttpGet]
    public IActionResult Create(int departmentId)
    {
        ViewData["DepartmentId"] = departmentId;

        return View(new NamedActiveEntityViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        int departmentId,
        NamedActiveEntityViewModel model)
    {
        if (!ModelState.IsValid)
        {
            ViewData["DepartmentId"] = departmentId;

            return View(model);
        }

        var result = await crudService.ExecuteAsync(
            CrudCommand.Create,
            properties:
            [
                new CrudProperty
                {
                    Name = nameof(Position.Name),
                    Value = model.Name
                },
                new CrudProperty
                {
                    Name = nameof(Position.Notes),
                    Value = model.Notes
                },
                new CrudProperty
                {
                    Name = nameof(Position.IsActive),
                    Value = model.IsActive
                },
                new CrudProperty
                {
                    Name = nameof(Position.DepartmentId),
                    Value = departmentId
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
                        $"Длъжността „{result.Name}“ беше създадена успешно."
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
        var position = await service.GetByIdAsync(id);

        if (position == null ||
            position.DepartmentId != departmentId)
        {
            return NotFound();
        }

        var model = new NamedActiveEntityViewModel
        {
            Name = position.Name,
            Notes = position.Notes,
            IsActive = position.IsActive
        };

        ViewData["Id"] = position.Id;
        ViewData["DepartmentId"] = departmentId;

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        int id,
        int departmentId,
        NamedActiveEntityViewModel model)
    {
        if (!ModelState.IsValid)
        {
            ViewData["Id"] = id;
            ViewData["DepartmentId"] = departmentId;

            return View(model);
        }

        var position = await service.GetByIdAsync(id);

        if (position == null ||
            position.DepartmentId != departmentId)
        {
            return NotFound();
        }

        var result = await crudService.ExecuteAsync(
            CrudCommand.Update,
            id,
            [
                new CrudProperty
                {
                    Name = nameof(Position.Name),
                    Value = model.Name
                },
                new CrudProperty
                {
                    Name = nameof(Position.Notes),
                    Value = model.Notes
                },
                new CrudProperty
                {
                    Name = nameof(Position.IsActive),
                    Value = model.IsActive
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
                        $"Длъжността „{result.Name}“ беше променена успешно."
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
                        "Длъжността не беше намерена и не беше променена."
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
        var position = await service.GetByIdAsync(id);

        if (position == null ||
            position.DepartmentId != departmentId)
        {
            TempData.SetCrudResult(
                new CrudResultViewModel
                {
                    Type = CrudResultType.Warning,
                    Title = "Внимание",
                    Message =
                        "Длъжността не беше намерена и не беше изтрита."
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
                        $"Длъжността „{result.Name}“ беше изтрита успешно."
                });
        }

        return RedirectToAction(
            nameof(Index),
            new { departmentId });
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
                        $"Длъжността „{result.Name}“ беше възстановена успешно."
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
                        "Длъжността не беше намерена и не беше възстановена."
                });
        }

        return RedirectToAction(nameof(Deleted));
    }
}