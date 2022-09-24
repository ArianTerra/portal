using EducationPortal.BusinessLogic.DTO;
using FluentResults;

namespace EducationPortal.BusinessLogic.Services.Interfaces;

public interface ISkillService
{
    Task<Result<SkillDto>> GetSkillByIdAsync(Guid id);

    Task<Result<IEnumerable<SkillDto>>> GetSkillsPageAsync(int page, int pageSize);

    Task<Result<int>> GetSkillsCountAsync();

    Task<Result<Guid>> AddSkillAsync(SkillDto dto, Guid createdById);

    Task<Result> UpdateSkillAsync(SkillDto dto, Guid updatedById);

    Task<Result> DeleteSkillByIdAsync(Guid id);
}