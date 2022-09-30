using EducationPortal.BusinessLogic.DTO;
using FluentResults;

namespace EducationPortal.BusinessLogic.Services.Interfaces;

public interface IMaterialProgressService
{
    Task<Result> SetProgressAsync(Guid materialId, Guid userId, int percent);

    Task<Result<IEnumerable<MaterialProgressDto>>> GetMaterialsProgressAsync(Guid userId);
}