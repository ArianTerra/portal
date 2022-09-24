using EducationPortal.BusinessLogic.DTO;
using FluentResults;

namespace EducationPortal.BusinessLogic.Services.Interfaces;

public interface ICourseService
{
    Task<Result<CourseDto>> GetCourseByIdAsync(Guid id);

    Task<Result<CourseDto>> GetCourseByNameAsync(string name);

    Task<Result<IEnumerable<CourseDto>>> GetCoursePageAsync(int page, int pageSize);

    Task<Result<int>> GetCoursesCountAsync();

    Task<Result<Guid>> AddCourseAsync(CourseDto dto, Guid createdById);

    Task<Result> UpdateCourseAsync(CourseDto dto, Guid updatedById);

    Task<Result> DeleteCourseByIdAsync(Guid id);

    Task<Result> AddMaterialsToCourseAsync(Guid courseId, IEnumerable<MaterialDto> materials);

    Task<Result> AddSkillsToCourseAsync(Guid id, IEnumerable<SkillDto> skills);
}