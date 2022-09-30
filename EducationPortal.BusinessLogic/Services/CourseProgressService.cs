using System.Linq.Expressions;
using EducationPortal.BusinessLogic.DTO;
using EducationPortal.BusinessLogic.Errors;
using EducationPortal.BusinessLogic.Services.Interfaces;
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

    private readonly IGenericRepository<SkillProgress> _skillProgressRepository;

    private readonly IGenericRepository<Skill> _skillRepository;

    private readonly UserManager<ApplicationUser> _userManager;

    private readonly IGenericRepository<Course> _courseRepository;

    private readonly IGenericRepository<Material> _materialRepository;

    public CourseProgressService(IGenericRepository<CourseProgress> courseProgressRepository, IGenericRepository<MaterialProgress> materialProgressRepository, UserManager<ApplicationUser> userManager, IGenericRepository<Course> courseRepository, IGenericRepository<Material> materialRepository, IGenericRepository<SkillProgress> skillProgressRepository, IGenericRepository<Skill> skillRepository)
    {
        _courseProgressRepository = courseProgressRepository;
        _materialProgressRepository = materialProgressRepository;
        _userManager = userManager;
        _courseRepository = courseRepository;
        _materialRepository = materialRepository;
        _skillProgressRepository = skillProgressRepository;
        _skillRepository = skillRepository;
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
                x => x.CourseMaterials,
                x => x.CourseSkills
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
            UserId = userId
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

        //subscribe to all course skills
        foreach (var cs in course.CourseSkills)
        {
            if (await _skillProgressRepository.FindFirstAsync(x => x.SkillId == cs.SkillId) == null)
            {
                await _skillProgressRepository.AddAsync(new SkillProgress()
                {
                    SkillId = cs.SkillId,
                    UserId = userId,
                    Level = 0
                });
            }
        }

        return Result.Ok();
    }

    public async Task<Result> UnsubscribeFromCourseAsync(Guid userId, Guid courseId)
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

        var link = await _courseProgressRepository.FindFirstAsync(
            x => x.UserId == userId && x.CourseId == courseId
        );

        if (link == null)
        {
            return Result.Fail(new BadRequestError("User is not subscribed to the course"));
        }

        await _courseProgressRepository.RemoveAsync(link);

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
                x => x.CourseMaterials,
                x => x.CourseSkills
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
            return Result.Fail(new NotFoundError(courseId));
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

        var skillProgressesIds = course.CourseSkills.Select(x => x.SkillId);
        var skillProgressDtos = new List<SkillProgressDto>();
        foreach (var id in skillProgressesIds)
        {
            var progress = await _skillProgressRepository.FindFirstAsync(x => x.SkillId == id);
            var skillName = (await _skillRepository.FindFirstAsync(x => x.Id == id)).Name;
            skillProgressDtos.Add(new SkillProgressDto
            {
                SkillId = id,
                Name = skillName,
                Level = progress.Level,
            });
        }

        int percent = 0;
        if (materialProgressDtos.Count != 0)
        {
            percent = materialProgressDtos.Select(x => x.Progress).Sum() / materialProgressDtos.Count;
        }

        var courseProgressDto = new CourseProgressDto
        {
            CourseId = courseId,
            CourseName = course.Name,
            Progress = percent,
            Materials = materialProgressDtos,
            Skills = skillProgressDtos
        };

        return courseProgressDto;
    }

    public async Task<Result> GiveSkillsToUser(Guid userId, Guid courseId)
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
                x => x.CourseSkills
            });

        if (course == null)
        {
            return Result.Fail(new InternalServerError("Course not found in database"));
        }

        var skillIds = course.CourseSkills.Select(x => x.SkillId);
        foreach (var skillId in skillIds)
        {
            var skillProgress = await _skillProgressRepository.FindFirstAsync(x => x.UserId == userId && x.SkillId == skillId);
            if (skillProgress == null)
            {
                await _skillProgressRepository.AddAsync(new SkillProgress()
                {
                    UserId = userId,
                    SkillId = skillId,
                    Level = 1
                });
            }
            else
            {
                skillProgress.Level += 1;
                await _skillProgressRepository.UpdateAsync(skillProgress);
            }
        }

        return Result.Ok();
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