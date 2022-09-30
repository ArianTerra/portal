using System.Security.Claims;
using EducationPortal.BusinessLogic.DTO;
using EducationPortal.BusinessLogic.Errors;
using EducationPortal.BusinessLogic.Extensions;
using EducationPortal.BusinessLogic.Services.Interfaces;
using EducationPortal.BusinessLogic.Utils.Comparers;
using EducationPortal.Presentation.ViewModels;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EducationPortal.Presentation.Controllers;

[Authorize]
public class BooksController : Controller
{
    private readonly IBookMaterialService _bookMaterialService;

    private readonly IBookFormatService _bookFormatService;

    private readonly IBookAuthorService _bookAuthorService;

    public BooksController(IBookMaterialService bookMaterialService, IBookFormatService bookFormatService, IBookAuthorService bookAuthorService)
    {
        _bookMaterialService = bookMaterialService;
        _bookFormatService = bookFormatService;
        _bookAuthorService = bookAuthorService;
    }

    public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
    {
        var result = await _bookMaterialService.GetBooksPageAsync(page, pageSize);
        var itemsCountResult = await _bookMaterialService.GetBooksCountAsync();

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

        var viewModel = new PageViewModel<BookMaterialDto>
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
        var result = await _bookMaterialService.GetBookByIdAsync(id);

        if (result.IsFailed)
        {
            return StatusCode(result.GetErrorCode());
        }

        return View(result.Value);
    }

    public async Task<IActionResult> Add()
    {
        var resultFormats = await _bookFormatService.GetAllBookFormatesAsync();
        var resultAuthors = await _bookAuthorService.GetAllBookAuthorsAsync();

        if (resultFormats.IsFailed)
        {
            return StatusCode(resultFormats.GetErrorCode());
        }

        if (resultAuthors.IsFailed)
        {
            return StatusCode(resultAuthors.GetErrorCode());
        }

        var viewModel = new BookViewModel
        {
            AvailableFormats = resultFormats.Value,
            AvailableAuthors = resultAuthors.Value
        };

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Add(BookViewModel viewModel)
    {
        var selectedFormat = await _bookFormatService.GetBookFormatByNameAsync(viewModel.Format);
        if (selectedFormat.IsFailed)
        {
            return StatusCode(selectedFormat.GetErrorCode());
        }

        var dto = new BookMaterialDto()
        {
            Id = viewModel.Id,
            Name = viewModel.Name,
            Pages = viewModel.Pages,
            BookFormat = selectedFormat.Value
        };

        var selectedAuthors = new List<BookAuthorDto>();
        foreach (var authorName in viewModel.SelectedAuthors)
        {
            var authorResult = await _bookAuthorService.GetBookAuthorByName(authorName);

            if (authorResult.IsFailed)
            {
                return StatusCode(authorResult.GetErrorCode());
            }

            selectedAuthors.Add(authorResult.Value);
        }

        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var result = await _bookMaterialService.AddBookAsync(dto, userId);

        if (result.IsFailed)
        {
            if (result.GetErrorCode() == (int)ErrorCode.ValidationError)
            {
                result.GetValidationResult().AddToModelState(this.ModelState);

                var resultFormats = await _bookFormatService.GetAllBookFormatesAsync();
                if (resultFormats.IsFailed)
                {
                    return StatusCode(resultFormats.GetErrorCode());
                }

                var resultAuthors = await _bookAuthorService.GetAllBookAuthorsAsync();
                if (resultAuthors.IsFailed)
                {
                    return StatusCode(resultAuthors.GetErrorCode());
                }

                viewModel.AvailableFormats = resultFormats.Value;
                viewModel.AvailableAuthors = resultAuthors.Value;

                return View(viewModel);
            }

            return StatusCode(result.GetErrorCode());
        }

        var resultAddingAuthors = await _bookMaterialService.AddAuthorsToBookAsync(result.Value, selectedAuthors);

        if (resultAddingAuthors.IsFailed)
        {
            return StatusCode(resultAddingAuthors.GetErrorCode());
        }

        return RedirectToAction("Details", new {id = result.Value});
    }

    public async Task<IActionResult> Edit(Guid id)
    {
        var result = await _bookMaterialService.GetBookByIdAsync(id);

        if (result.IsFailed)
        {
            return StatusCode(result.GetErrorCode());
        }

        var resultAuthors = await _bookAuthorService.GetAllBookAuthorsAsync();

        if (resultAuthors.IsFailed)
        {
            return StatusCode(resultAuthors.GetErrorCode());
        }

        var resultFormats = await _bookFormatService.GetAllBookFormatesAsync();

        if (resultFormats.IsFailed)
        {
            return StatusCode(resultFormats.GetErrorCode());
        }

        var viewModel = new BookViewModel()
        {
            Id = result.Value.Id,
            Name = result.Value.Name,
            Pages = result.Value.Pages,
            Format = result.Value.BookFormat.Name,
            AvailableFormats = resultFormats.Value,
            AvailableAuthors = resultAuthors.Value.Except(result.Value.Authors, new BookAuthorDtoComparer()),
            SelectedAuthors = result.Value.Authors.Select(x => x.Name)
        };

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(BookViewModel viewModel)
    {
        var selectedFormat = await _bookFormatService.GetBookFormatByNameAsync(viewModel.Format);
        if (selectedFormat.IsFailed)
        {
            return StatusCode(selectedFormat.GetErrorCode());
        }

        var dto = new BookMaterialDto()
        {
            Id = viewModel.Id,
            Name = viewModel.Name,
            Pages = viewModel.Pages,
            BookFormat = selectedFormat.Value
        };

        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var result = await _bookMaterialService.UpdateBookAsync(dto, userId);

        if (result.IsFailed)
        {
            if (result.GetErrorCode() == (int)ErrorCode.ValidationError)
            {
                result.GetValidationResult().AddToModelState(this.ModelState);
                var resultFormats = await _bookFormatService.GetAllBookFormatesAsync();

                if (resultFormats.IsFailed)
                {
                    return StatusCode(resultFormats.GetErrorCode());
                }

                viewModel.AvailableFormats = resultFormats.Value;

                return View(viewModel);
            }

            return StatusCode(result.GetErrorCode());
        }

        var selectedAuthors = new List<BookAuthorDto>();
        foreach (var authorName in viewModel.SelectedAuthors)
        {
            var authorResult = await _bookAuthorService.GetBookAuthorByName(authorName);

            if (authorResult.IsFailed)
            {
                return StatusCode(authorResult.GetErrorCode());
            }

            selectedAuthors.Add(authorResult.Value);
        }

        var resultAddingAuthors = await _bookMaterialService.AddAuthorsToBookAsync(viewModel.Id, selectedAuthors);

        if (resultAddingAuthors.IsFailed)
        {
            return StatusCode(resultAddingAuthors.GetErrorCode());
        }

        return RedirectToAction("Details", new {id = dto.Id});
    }

    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _bookMaterialService.DeleteBookByIdAsync(id);

        if (result.IsFailed)
        {
            return StatusCode(result.GetErrorCode());
        }

        return RedirectToAction("Index");
    }
}