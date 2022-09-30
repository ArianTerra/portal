using System.Security.Claims;
using AutoMapper;
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
public class CoursesController : Controller
{
    private readonly ICourseService _courseService;

    private readonly IMaterialService _materialService;

    private readonly ISkillService _skillService;

    private readonly IMapper _mapper;

    public CoursesController(ICourseService courseService, IMaterialService materialService, IMapper mapper, ISkillService skillService)
    {
        _courseService = courseService;
        _materialService = materialService;
        _mapper = mapper;
        _skillService = skillService;
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
        var availableSkills = await _skillService.GetAllSkillsAsync();

        if (availableSkills.IsFailed)
        {
            return StatusCode(availableSkills.GetErrorCode());
        }

        return View(new CourseViewModel()
        {
            AvailableSkills = availableSkills.Value
        });
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

        var selectedSkills = new List<SkillDto>();
        foreach (var skillName in viewModel.SelectedSkills)
        {
            var skillResult = await _skillService.GetSkillByNameAsync(skillName);

            if (skillResult.IsFailed)
            {
                return StatusCode(skillResult.GetErrorCode());
            }

            selectedSkills.Add(skillResult.Value);
        }

        var dto = _mapper.Map<CourseDto>(viewModel);
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

        var result = await _courseService.AddCourseAsync(dto, userId);

        if (result.IsFailed)
        {
            if (result.GetErrorCode() == (int)ErrorCode.ValidationError)
            {
                result.GetValidationResult().AddToModelState(this.ModelState);
                var availableSkills = await _skillService.GetAllSkillsAsync();

                if (availableSkills.IsFailed)
                {
                    return StatusCode(availableSkills.GetErrorCode());
                }

                viewModel.AvailableSkills = availableSkills.Value;

                return View(viewModel);
            }

            return StatusCode(result.GetErrorCode());
        }

        var resultAddedMaterials = await _courseService.AddMaterialsToCourseAsync(result.Value, selectedMaterials);

        if (resultAddedMaterials.IsFailed)
        {
            return StatusCode(resultAddedMaterials.GetErrorCode());
        }

        var skillAddedResult = await _courseService.AddSkillsToCourseAsync(result.Value, selectedSkills);

        if (skillAddedResult.IsFailed)
        {
            return StatusCode(skillAddedResult.GetErrorCode());
        }

        return RedirectToAction("Details", new {id = result.Value});
    }

    public async Task<IActionResult> Edit(Guid id)
    {
        var result = await _courseService.GetCourseByIdAsync(id);

        if (result.IsFailed)
        {
            return StatusCode(result.GetErrorCode());
        }

        var availableSkills = await _skillService.GetAllSkillsAsync();

        if (availableSkills.IsFailed)
        {
            return StatusCode(availableSkills.GetErrorCode());
        }

        var viewModel = _mapper.Map<CourseViewModel>(result.Value);
        viewModel.AvailableSkills = availableSkills.Value.Except(result.Value.Skills, new SkillDtoComparer());
        viewModel.SelectedSkills = result.Value.Skills.Select(x => x.Name);

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(CourseViewModel viewModel)
    {
        var dto = _mapper.Map<CourseDto>(viewModel);

        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var result = await _courseService.UpdateCourseAsync(dto, userId);

        if (result.IsFailed)
        {
            if (result.GetErrorCode() == (int)ErrorCode.ValidationError)
            {
                result.GetValidationResult().AddToModelState(this.ModelState);
                var availableSkills = await _skillService.GetAllSkillsAsync();

                if (availableSkills.IsFailed)
                {
                    return StatusCode(availableSkills.GetErrorCode());
                }

                viewModel.AvailableSkills = availableSkills.Value;

                return View(viewModel);
            }
        }

        var selectedMaterials = new List<MaterialDto>();
        foreach (var material in viewModel.SelectedMaterials)
        {
            var materialResult = await _materialService.GetMaterialByIdAsync(Guid.Parse(material));

            if (materialResult.IsFailed)
            {
                return StatusCode(materialResult.GetErrorCode());
            }

            selectedMaterials.Add(materialResult.Value);
        }

        var selectedSkills = new List<SkillDto>();
        foreach (var skillName in viewModel.SelectedSkills)
        {
            var skillResult = await _skillService.GetSkillByNameAsync(skillName);

            if (skillResult.IsFailed)
            {
                return StatusCode(skillResult.GetErrorCode());
            }

            selectedSkills.Add(skillResult.Value);
        }

        var resultAddingMaterials = await _courseService.AddMaterialsToCourseAsync(viewModel.Id, selectedMaterials);
        if (resultAddingMaterials.IsFailed)
        {
            return StatusCode(resultAddingMaterials.GetErrorCode());
        }

        var skillAddedResult = await _courseService.AddSkillsToCourseAsync(viewModel.Id, selectedSkills);

        if (skillAddedResult.IsFailed)
        {
            return StatusCode(skillAddedResult.GetErrorCode());
        }

        return RedirectToAction("Details", new {id = dto.Id});
    }

    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _courseService.DeleteCourseByIdAsync(id);

        if (result.IsFailed)
        {
            return StatusCode(result.GetErrorCode());
        }

        return RedirectToAction("Index");
    }
}