using EducationPortal.BusinessLogic.DTO;
using EducationPortal.BusinessLogic.Errors;
using EducationPortal.BusinessLogic.Extensions;
using EducationPortal.BusinessLogic.Services.Interfaces;
using EducationPortal.Presentation.ViewModels;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;

namespace EducationPortal.Presentation.Controllers;

public class VideosController : Controller
{
    private readonly IVideoMaterialService _videoMaterialService;

    private readonly IVideoQualityService _videoQualityService;

    public VideosController(IVideoMaterialService videoMaterialService, IVideoQualityService videoQualityService)
    {
        _videoMaterialService = videoMaterialService;
        _videoQualityService = videoQualityService;
    }

    public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
    {
        var result = await _videoMaterialService.GetVideosPageAsync(page, pageSize);
        var itemsCountResult = await _videoMaterialService.GetVideosCountAsync();

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

        var viewModel = new PageViewModel<VideoMaterialDto>
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
        var result = await _videoMaterialService.GetVideoByIdAsync(id);

        if (result.IsFailed)
        {
            return StatusCode(result.GetErrorCode());
        }

        return View(result.Value);
    }

    public async Task<IActionResult> Add()
    {
        var resultQualities = await _videoQualityService.GetAllVideoQualitiesAsync();

        if (resultQualities.IsFailed)
        {
            return StatusCode(resultQualities.GetErrorCode());
        }

        var viewModel = new VideoViewModel
        {
            AvailableQualities = resultQualities.Value
        };

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Add(VideoViewModel viewModel)
    {
        var selectedQuality = await _videoQualityService.GetVideoQualityByNameAsync(viewModel.Quality);
        if (selectedQuality.IsFailed)
        {
            return StatusCode(selectedQuality.GetErrorCode());
        }

        var dto = new VideoMaterialDto
        {
            Id = viewModel.Id,
            Name = viewModel.Name,
            Duration = viewModel.Duration,
            Quality = selectedQuality.Value
        };

        var result = await _videoMaterialService.AddVideoAsync(dto);

        if (result.IsFailed)
        {
            if (result.GetErrorCode() == (int)ErrorCode.ValidationError)
            {
                result.GetValidationResult().AddToModelState(this.ModelState);

                return View(viewModel);
            }

            return StatusCode(result.GetErrorCode());
        }

        return RedirectToAction("Details", new {id = result.Value});
    }

    public async Task<IActionResult> Edit(Guid id)
    {
        var result = await _videoMaterialService.GetVideoByIdAsync(id);

        if (result.IsFailed)
        {
            return StatusCode(result.GetErrorCode());
        }

        var resultQualities = await _videoQualityService.GetAllVideoQualitiesAsync();

        if (resultQualities.IsFailed)
        {
            return StatusCode(resultQualities.GetErrorCode());
        }

        var viewModel = new VideoViewModel
        {
            Id = result.Value.Id,
            Name = result.Value.Name,
            Duration = result.Value.Duration,
            Quality = result.Value.Quality.Name,
            AvailableQualities = resultQualities.Value
        };

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(VideoViewModel viewModel)
    {
        var selectedQuality = await _videoQualityService.GetVideoQualityByNameAsync(viewModel.Quality);
        if (selectedQuality.IsFailed)
        {
            return StatusCode(selectedQuality.GetErrorCode());
        }

        var dto = new VideoMaterialDto
        {
            Id = viewModel.Id,
            Name = viewModel.Name,
            Duration = viewModel.Duration,
            Quality = selectedQuality.Value
        };

        var result = await _videoMaterialService.UpdateVideoAsync(dto);

        if (result.IsFailed)
        {
            if (result.GetErrorCode() == (int)ErrorCode.ValidationError)
            {
                result.GetValidationResult().AddToModelState(this.ModelState);
                var resultQualities = await _videoQualityService.GetAllVideoQualitiesAsync();

                if (resultQualities.IsFailed)
                {
                    return StatusCode(resultQualities.GetErrorCode());
                }

                viewModel.AvailableQualities = resultQualities.Value;

                return View(viewModel);
            }

            return StatusCode(result.GetErrorCode());
        }

        return RedirectToAction("Details", new {id = dto.Id});
    }

    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _videoMaterialService.DeleteVideoByIdAsync(id);

        if (result.IsFailed)
        {
            return StatusCode(result.GetErrorCode());
        }

        return RedirectToAction("Index");
    }
}