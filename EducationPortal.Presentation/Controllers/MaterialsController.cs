using EducationPortal.BusinessLogic.DTO;
using EducationPortal.BusinessLogic.Extensions;
using EducationPortal.BusinessLogic.Services.Interfaces;
using EducationPortal.Presentation.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EducationPortal.Presentation.Controllers;

[Authorize]
public class MaterialsController : Controller
{
    private readonly IMaterialService _materialService;

    public MaterialsController(IMaterialService materialService)
    {
        _materialService = materialService;
    }

    public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
    {
        var result = await _materialService.GetMaterialsPageAsync(page, pageSize);
        var itemsCountResult = await _materialService.GetMaterialsCountAsync();

        if (result.IsFailed)
        {
            return StatusCode(result.GetErrorCode());
        }

        if (itemsCountResult.IsFailed)
        {
            return StatusCode(itemsCountResult.GetErrorCode());
        }

        var pageCount = (int)Math.Ceiling((double)itemsCountResult.Value / pageSize);
        if (pageCount <= 0)
        {
            pageCount = 1;
        }

        var viewModel = new PageViewModel<MaterialDto>
        {
            Items = result.Value.ToList(),
            Page = page,
            PageSize = pageSize,
            PageCount = pageCount
        };

        return View(viewModel);
    }

    public async Task<IActionResult> Details(Guid id)
    {
        var result = await _materialService.GetMaterialByIdAsync(id);

        if (result.IsFailed)
        {
            return StatusCode(result.GetErrorCode());
        }

        return RedirectToMaterialType(result.Value.Discriminator, "Details", id);
    }

    public async Task<IActionResult> Edit(Guid id)
    {
        var result = await _materialService.GetMaterialByIdAsync(id);

        if (result.IsFailed)
        {
            return StatusCode(result.GetErrorCode());
        }

        return RedirectToMaterialType(result.Value.Discriminator, "Edit", id);
    }

    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _materialService.GetMaterialByIdAsync(id);

        if (result.IsFailed)
        {
            return StatusCode(result.GetErrorCode());
        }

        return RedirectToMaterialType(result.Value.Discriminator, "Delete", id);
    }

    private IActionResult RedirectToMaterialType(string discriminator, string action, Guid id)
    {
        if (discriminator == "ArticleMaterial")
        {
            return RedirectToAction(action, "Articles", new { id });
        }
        else if (discriminator == "BookMaterial")
        {
            return RedirectToAction(action, "Books", new { id });
        }
        else if (discriminator == "VideoMaterial")
        {
            return RedirectToAction(action, "Videos", new { id });
        }
        else
        {
            throw new ArgumentException("Unknown type of DTO");
        }
    }
}