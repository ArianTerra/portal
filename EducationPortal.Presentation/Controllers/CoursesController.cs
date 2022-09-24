using EducationPortal.BusinessLogic.Services.Interfaces;
using EducationPortal.Presentation.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EducationPortal.Presentation.Controllers;

public class CoursesController : Controller
{
    private readonly ICourseService _courseService;

    //private readonly

    public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
    {
        throw new NotImplementedException();
    }

    public async Task<IActionResult> Details(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<IActionResult> Add()
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    public async Task<IActionResult> Add(CourseViewModel viewModel)
    {
        throw new NotImplementedException();
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