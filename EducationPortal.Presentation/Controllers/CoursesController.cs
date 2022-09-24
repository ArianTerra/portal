using System.Security.Claims;
using AutoMapper;
using EducationPortal.BusinessLogic.DTO;
using EducationPortal.BusinessLogic.Errors;
using EducationPortal.BusinessLogic.Extensions;
using EducationPortal.BusinessLogic.Services.Interfaces;
using EducationPortal.Presentation.ViewModels;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EducationPortal.Presentation.Controllers;

[Authorize]
public class CoursesController : Controller
{
    private readonly ICourseService _courseService;

    private readonly IMaterialService _materialService;

    private readonly IMapper _mapper;

    public CoursesController(ICourseService courseService, IMaterialService materialService, IMapper mapper)
    {
        _courseService = courseService;
        _materialService = materialService;
        _mapper = mapper;
    }

    public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
    {
        var result = await _courseService.GetCoursePageAsync(page, pageSize);
        var itemsCountResult = await _courseService.GetCoursesCountAsync();

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

        var viewModel = new PageViewModel<CourseDto>
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
        var result = await _courseService.GetCourseByIdAsync(id);

        if (result.IsFailed)
        {
            return StatusCode(result.GetErrorCode());
        }

        return View(result.Value);
    }

    public async Task<IActionResult> Add()
    {
        return View(new CourseViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> Add(CourseViewModel viewModel)
    {
        var selectedMaterials = new List<MaterialDto>();
        foreach (var materialId in viewModel.SelectedMaterials)
        {
            var materialResult = await _materialService.GetMaterialByIdAsync(Guid.Parse(materialId));

            if (materialResult.IsFailed)
            {
                return StatusCode(materialResult.GetErrorCode());
            }

            selectedMaterials.Add(materialResult.Value);
        }

        var dto = _mapper.Map<CourseDto>(viewModel);
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

        var result = await _courseService.AddCourseAsync(dto, userId);

        if (result.IsFailed)
        {
            if (result.GetErrorCode() == (int)ErrorCode.ValidationError)
            {
                result.GetValidationResult().AddToModelState(this.ModelState);

                return View(viewModel);
            }

            return StatusCode(result.GetErrorCode());
        }

        var resultAddedMaterials = await _courseService.AddMaterialsToCourseAsync(result.Value, selectedMaterials);

        if (resultAddedMaterials.IsFailed)
        {
            return StatusCode(resultAddedMaterials.GetErrorCode());
        }

        return RedirectToAction("Details", new {id = result.Value});
    }

    public async Task<IActionResult> Edit(Guid id)
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    public async Task<IActionResult> Edit(BookViewModel viewModel)
    {
        throw new NotImplementedException();
    }

    public async Task<IActionResult> Delete(Guid id)
    {
        throw new NotImplementedException();
    }
}