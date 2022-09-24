using EducationPortal.BusinessLogic.DTO;
using FluentResults;

namespace EducationPortal.BusinessLogic.Services.Interfaces;

public interface IMaterialService
{
    Task<Result<MaterialDto>> GetMaterialByIdAsync(Guid id);

    Task<Result<IEnumerable<MaterialDto>>> GetMaterialsPageAsync(int page, int pageSize);

    Task<Result<int>> GetMaterialsCountAsync();
}