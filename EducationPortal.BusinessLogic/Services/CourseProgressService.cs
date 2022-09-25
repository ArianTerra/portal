using System.Linq.Expressions;
using EducationPortal.BusinessLogic.DTO;
using EducationPortal.BusinessLogic.Errors;
using EducationPortal.DataAccess.DomainModels;
using EducationPortal.DataAccess.DomainModels.Progress;
using EducationPortal.DataAccess.Repositories;
using FluentResults;
using Microsoft.AspNetCore.Identity;

namespace EducationPortal.BusinessLogic.Services;

public class CourseProgressService : ICourseProgressService
{
    private readonly IGenericRepository<CourseProgress> _courseProgressRepository;

    private readonly IGenericRepository<MaterialProgress> _materialProgressRepository;

    private UserManager<ApplicationUser> _userManager;

    private IGenericRepository<Course> _courseRepository;

    private IGenericRepository<Material> _materialRepository;

    public CourseProgressService(IGenericRepository<CourseProgress> courseProgressRepository, IGenericRepository<MaterialProgress> materialProgressRepository, UserManager<ApplicationUser> userManager, IGenericRepository<Course> courseRepository, IGenericRepository<Material> materialRepository)
    {
        _courseProgressRepository = courseProgressRepository;
        _materialProgressRepository = materialProgressRepository;
        _userManager = userManager;
        _courseRepository = courseRepository;
        _materialRepository = materialRepository;
    }

    public async Task<Result> SubscribeToCourseAsync(Guid userId, Guid courseId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null)
        {
            return Result.Fail(new InternalServerError("User not found in database"));
        }

        var course = await _courseRepository.FindFirstAsync(
            x => x.Id == courseId,
            includes: new Expression<Func<Course, object>>[]
            {
                x => x.CourseMaterials
            });
        if (course == null)
        {
            return Result.Fail(new InternalServerError("Course not found in database"));
        }

        var link = await _courseProgressRepository.FindFirstAsync(
            x => x.UserId == userId && x.CourseId == courseId
        );

        if (link != null)
        {
            return Result.Fail(new BadRequestError("Subscription already exists"));
        }

        var subscription = new CourseProgress()
        {
            CourseId = courseId,
            UserId = userId,
            Progress = 0
        };

        await _courseProgressRepository.AddAsync(subscription);

        //subscribe to all course materials
        foreach (var cm in course.CourseMaterials)
        {
            if (await _materialProgressRepository.FindFirstAsync(x => x.MaterialId == cm.MaterialId) == null)
            {
                await _materialProgressRepository.AddAsync(new MaterialProgress()
                {
                    MaterialId = cm.MaterialId,
                    UserId = userId,
                    Progress = 0
                });
            }
        }

        return Result.Ok();
    }

    public async Task<Result<bool>> CheckSubscriptionExistAsync(Guid userId, Guid courseId)
    {
        var checkCourseUser = await CheckCourseUserAsync(userId, courseId);
        if (checkCourseUser.IsFailed)
        {
            return checkCourseUser;
        }

        var link = await _courseProgressRepository.FindFirstAsync(
            x => x.UserId == userId && x.CourseId == courseId
        );

        return link != null;
    }

    public async Task<Result<CourseProgressDto>> GetCourseProgress(Guid userId, Guid courseId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null)
        {
            return Result.Fail(new InternalServerError("User not found in database"));
        }

        var course = await _courseRepository.FindFirstAsync(
            x => x.Id == courseId,
            includes: new Expression<Func<Course, object>>[]
            {
                x => x.CourseMaterials
            });

        if (course == null)
        {
            return Result.Fail(new InternalServerError("Course not found in database"));
        }

        var link = await _courseProgressRepository.FindFirstAsync(
            x => x.UserId == userId && x.CourseId == courseId
        );

        if (link == null)
        {
            return Result.Fail(new BadRequestError("User is not subscribed to the course"));
        }

        var materialProgressesIds = course.CourseMaterials.Select(x => x.MaterialId);
        var materialProgressDtos = new List<MaterialProgressDto>();
        foreach (var id in materialProgressesIds)
        {
            var progress = await _materialProgressRepository.FindFirstAsync(x => x.MaterialId == id);
            var materialName = (await _materialRepository.FindFirstAsync(x => x.Id == id)).Name;
            materialProgressDtos.Add(new MaterialProgressDto()
            {
                MaterialId = id,
                Name = materialName,
                Progress = progress.Progress,
            });
        }

        var courseProgressDto = new CourseProgressDto()
        {
            CourseId = courseId,
            CourseName = course.Name,
            Progress = 0, //todo
            Materials = materialProgressDtos
        };

        return courseProgressDto;
    }

    private async Task<Result> CheckCourseUserAsync(Guid userId, Guid courseId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null)
        {
            return Result.Fail(new InternalServerError("User not found in database"));
        }

        var course = await _courseRepository.FindFirstAsync(x => x.Id == courseId);
        if (course == null)
        {
            return Result.Fail(new InternalServerError("Course not found in database"));
        }

        return Result.Ok();
    }
}