using EducationPortal.BusinessLogic.DTO;
using EducationPortal.BusinessLogic.Extensions;
using EducationPortal.BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EducationPortal.Presentation.Controllers.API;

[Authorize]
[Route("/api/materials")]
public class MaterialApiController : ControllerBase
{
    private readonly IMaterialService _materialService;

    public MaterialApiController(IMaterialService materialService)
    {
        _materialService = materialService;
    }

    public async Task<IActionResult> Get(int page = 1, int pageSize = 10, string nameStartsWith = "")
    {
        var result = await _materialService.GetMaterialsPageAsync(page, pageSize, nameStartsWith);
        var countResult = await _materialService.GetMaterialsCountAsync(nameStartsWith);

        if (result.IsFailed)
        {
            return StatusCode(result.GetErrorCode(), result.Errors);
        }

        return Ok(new
        {
            items = result.Value,
            items_count = countResult.Value
        });
    }
}

