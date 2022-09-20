using EducationPortal.BusinessLogic.DTO;
using EducationPortal.BusinessLogic.Errors;
using EducationPortal.BusinessLogic.Extensions;
using EducationPortal.BusinessLogic.Services.Interfaces;
using EducationPortal.Presentation.ViewModels;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;

namespace EducationPortal.Presentation.Controllers;

public class VideoQualitiesController : Controller
{
    private readonly IVideoQualityService _videoQualityService;

    public VideoQualitiesController(IVideoQualityService videoQualityService)
    {
        _videoQualityService = videoQualityService;
    }

    public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
    {
        var result = await _videoQualityService.GetVideoQualityPageAsync(page, pageSize);
        var itemsCountResult = await _videoQualityService.GetVideoQualitiesCountAsync();

        if (result.IsFailed)
        {
            return StatusCode(result.GetErrorCode());
        }

        if (itemsCountResult.IsFailed)
        {
            return StatusCode(itemsCountResult.GetErrorCode());
        }

        var pageCount = (int)Math.Ceiling((double)itemsCountResult.Value / pageSize);
        if (pageCount == 0)
        {
            pageCount = 1;
        }

        var viewModel = new PageViewModel<VideoQualityDto>()
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
        var result = await _videoQualityService.GetVideoQualityByIdAsync(id);

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
    public async Task<IActionResult> Add(VideoQualityDto dto)
    {
        var result = await _videoQualityService.AddVideoQualityAsync(dto);

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
        var result = await _videoQualityService.GetVideoQualityByIdAsync(id);

        if (result.IsFailed)
        {
            return StatusCode(result.GetErrorCode());
        }

        return View(result.Value);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(VideoQualityDto dto)
    {
        var result = await _videoQualityService.UpdateVideoQualityAsync(dto);

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
        var result = await _videoQualityService.DeleteVideoQualityByIdAsync(id);

        if (result.IsFailed)
        {
            return StatusCode(result.GetErrorCode());
        }

        return Redirect(Request.Headers["Referer"].ToString());
    }
}