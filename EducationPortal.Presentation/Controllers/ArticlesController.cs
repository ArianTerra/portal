using System.Security.Claims;
using EducationPortal.BusinessLogic.DTO;
using EducationPortal.BusinessLogic.Errors;
using EducationPortal.BusinessLogic.Extensions;
using EducationPortal.BusinessLogic.Services.Interfaces;
using EducationPortal.Presentation.ViewModels;
using Microsoft.AspNetCore.Mvc;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;

namespace EducationPortal.Presentation.Controllers;

[Authorize]
public class ArticlesController : Controller
{
    private readonly IArticleMaterialService _articleMaterialService;

    public ArticlesController(IArticleMaterialService articleMaterialService)
    {
        _articleMaterialService = articleMaterialService;
    }

    public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
    {
        var result = await _articleMaterialService.GetArticlesPageAsync(page, pageSize);
        var itemsCountResult = await _articleMaterialService.GetArticlesCountAsync();

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

        var viewModel = new PageViewModel<ArticleMaterialDto>()
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
        var result = await _articleMaterialService.GetArticleByIdAsync(id);

        if (result.IsFailed)
        {
            return StatusCode(result.GetErrorCode());
        }

        return View(result.Value);
    }

    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Add(ArticleMaterialDto dto)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

        var result = await _articleMaterialService.AddArticleAsync(dto, userId);

        if (result.IsFailed)
        {
            if (result.GetErrorCode() == (int)ErrorCode.ValidationError)
            {
                result.GetValidationResult().AddToModelState(this.ModelState);

                return View(dto);
            }

            return StatusCode(result.GetErrorCode());
        }

        return RedirectToAction("Details", new {id = result.Value});
    }

    public async Task<IActionResult> Edit(Guid id)
    {
        var result = await _articleMaterialService.GetArticleByIdAsync(id);

        if (result.IsFailed)
        {
            return StatusCode(result.GetErrorCode());
        }

        return View(result.Value);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(ArticleMaterialDto dto)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

        var result = await _articleMaterialService.UpdateArticleAsync(dto, userId);

        if (result.IsFailed)
        {
            if (result.GetErrorCode() == (int)ErrorCode.ValidationError)
            {
                result.GetValidationResult().AddToModelState(this.ModelState);

                return View(dto);
            }

            return StatusCode(result.GetErrorCode());
        }

        return RedirectToAction("Details", new {id = dto.Id});
    }

    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _articleMaterialService.DeleteArticleByIdAsync(id);

        if (result.IsFailed)
        {
            return StatusCode(result.GetErrorCode());
        }

        return RedirectToAction("Index");
    }
}