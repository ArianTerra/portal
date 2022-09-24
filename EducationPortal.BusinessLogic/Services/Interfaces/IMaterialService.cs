using EducationPortal.BusinessLogic.DTO;
using FluentResults;

namespace EducationPortal.BusinessLogic.Services.Interfaces;

public interface IMaterialService
{
    Task<Result<MaterialDto>> GetMaterialByIdAsync(Guid id);

    Task<Result<IEnumerable<MaterialDto>>> GetMaterialsPageAsync(int page, int pageSize, string? nameStartsWith = null);

    Task<Result<int>> GetMaterialsCountAsync(string? nameStartsWith = null);
}