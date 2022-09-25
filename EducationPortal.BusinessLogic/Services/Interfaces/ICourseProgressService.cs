using EducationPortal.BusinessLogic.DTO;
using FluentResults;

namespace EducationPortal.BusinessLogic.Services;

public interface ICourseProgressService
{
    Task<Result> SubscribeToCourseAsync(Guid userId, Guid courseId);

    Task<Result<bool>> CheckSubscriptionExistAsync(Guid userId, Guid courseId);

    Task<Result<CourseProgressDto>> GetCourseProgress(Guid userId, Guid courseId);
}