using EducationPortal.BusinessLogic.Services.Interfaces;
using EducationPortal.Presentation.ViewModels;
using Microsoft.AspNetCore.Mvc;

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
        var articles = await _articleMaterialService.GetArticlesPage(page, pageSize);

        var viewModel = new ArticlesViewModel()
        {
            Items = articles.Value,
            Page = page,
            PageSize = pageSize
        };

        return View(viewModel);
    }
}