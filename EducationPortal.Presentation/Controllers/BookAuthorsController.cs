using EducationPortal.BusinessLogic.DTO;
using EducationPortal.BusinessLogic.Errors;
using EducationPortal.BusinessLogic.Extensions;
using EducationPortal.BusinessLogic.Services.Interfaces;
using EducationPortal.Presentation.ViewModels;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;

namespace EducationPortal.Presentation.Controllers;

public class BookAuthorsController : Controller
{
    private readonly IBookAuthorService _bookAuthorService;

    public BookAuthorsController(IBookAuthorService bookAuthorService)
    {
        _bookAuthorService = bookAuthorService;
    }

    public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
    {
        var result = await _bookAuthorService.GetBookAuthorsPageAsync(page, pageSize);
        var itemsCountResult = await _bookAuthorService.GetBookAuthorCountAsync();

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

        var viewModel = new PageViewModel<BookAuthorDto>()
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
        var result = await _bookAuthorService.GetBookAuthorById(id);

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
    public async Task<IActionResult> Add(BookAuthorDto dto)
    {
        var result = await _bookAuthorService.AddBookAuthorAsync(dto);

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
        var result = await _bookAuthorService.GetBookAuthorById(id);

        if (result.IsFailed)
        {
            return StatusCode(result.GetErrorCode());
        }

        return View(result.Value);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(BookAuthorDto dto)
    {
        var result = await _bookAuthorService.UpdateBookAuthorAsync(dto);

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
        var result = await _bookAuthorService.DeleteBookAuthorByIdAsync(id);

        if (result.IsFailed)
        {
            return StatusCode(result.GetErrorCode());
        }

        return RedirectToAction("Index");
    }
}