using EducationPortal.BusinessLogic.DTO;
using EducationPortal.BusinessLogic.Extensions;
using EducationPortal.BusinessLogic.Services.Interfaces;
using EducationPortal.Presentation.ViewModels;
using Microsoft.AspNetCore.Mvc;
using FluentValidation.AspNetCore;

namespace EducationPortal.Presentation.Controllers;

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

        if (result.IsFailed)
        {
            return StatusCode(result.GetErrorCode());
        }

        var viewModel = new PageViewModel<ArticleMaterialDto>()
        {
            Items = result.Value,
            Page = page,
            PageSize = pageSize
        };

        return View(viewModel);
    }

    public async Task<IActionResult> ArticleDetails(Guid id)
    {
        var result = await _articleMaterialService.GetArticleByIdAsync(id);

        if (result.IsFailed)
        {
            return StatusCode(result.GetErrorCode());
        }

        return View(result.Value);
    }

    public IActionResult AddArticle()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddArticle(ArticleMaterialDto dto)
    {
        var result = await _articleMaterialService.AddArticleAsync(dto);

        if (result.IsFailed)
        {
            if (result.GetErrorCode() == 422)
            {
                result.GetValidationResult().AddToModelState(this.ModelState);

                return View(dto);
            }

            return StatusCode(result.GetErrorCode());
        }

        return RedirectToAction("ArticleDetails", new {id = result.Value});
    }

    public async Task<IActionResult> EditArticle(Guid id)
    {
        var result = await _articleMaterialService.GetArticleByIdAsync(id);

        if (result.IsFailed)
        {
            return StatusCode(result.GetErrorCode());
        }

        return View(result.Value);
    }

    [HttpPost]
    public async Task<IActionResult> EditArticle(ArticleMaterialDto dto)
    {
        var result = await _articleMaterialService.UpdateArticleAsync(dto);

        if (result.IsFailed)
        {
            if (result.GetErrorCode() == 422)
            {
                result.GetValidationResult().AddToModelState(this.ModelState);

                return View(dto);
            }

            return StatusCode(result.GetErrorCode());
        }

        return RedirectToAction("ArticleDetails", new {id = dto.Id});
    }

    public async Task<IActionResult> DeleteArticle(Guid id)
    {
        var result = await _articleMaterialService.DeleteArticleByIdAsync(id);

        if (result.IsFailed)
        {
            return StatusCode(result.GetErrorCode());
        }

        return Redirect(Request.Headers["Referer"].ToString());
    }
}