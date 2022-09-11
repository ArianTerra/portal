using EducationPortalConsole.BusinessLogic.Resources.ErrorMessages;
using EducationPortalConsole.BusinessLogic.Utils.Comparers;
using EducationPortalConsole.BusinessLogic.Validators.FluentValidation;
using EducationPortalConsole.Core.Entities;
using EducationPortalConsole.Core.Entities.JoinEntities;
using EducationPortalConsole.DataAccess.Repositories;
using FluentResults;
using FluentValidation;

namespace EducationPortalConsole.BusinessLogic.Services;

public class CourseService
{
    private readonly IGenericRepository<Course> _courseRepository;

    private readonly IGenericRepository<CourseMaterial> _courseMaterialRepository;

    private readonly IGenericRepository<CourseSkill> _courseSkillRepository;

    public CourseService()
    {
        _courseRepository = new GenericRepository<Course>();
        _courseMaterialRepository = new GenericRepository<CourseMaterial>();
        _courseSkillRepository = new GenericRepository<CourseSkill>();
    }

    public CourseService(IGenericRepository<Course> courseRepository, IGenericRepository<CourseMaterial> courseMaterialRepository, IGenericRepository<CourseSkill> courseSkillRepository)
    {
        _courseRepository = courseRepository;
        _courseMaterialRepository = courseMaterialRepository;
        _courseSkillRepository = courseSkillRepository;
    }

    public Result<Course> GetCourseById(Guid id)
    {
        if (id == Guid.Empty)
        {
            return Result.Fail(ErrorMessages.GuidEmpty);
        }

        var result = Result.Try(() =>
            _courseRepository.FindFirst(
            x => x.Id == id,
            true,
            x => x.CreatedBy,
            x => x.UpdatedBy,
            x => x.CourseMaterials,
            x => x.CourseSkills));

        if (result.IsSuccess && result.Value == null)
        {
            return Result.Fail(ErrorMessages.NotFound);
        }

        return result;
    }

    public Result<List<Course>> GetAllCourses()
    {
        var result = Result.Try(() =>
            _courseRepository.FindAll(
            _ => true, //todo remove this
            false,
            x => x.CreatedBy,
            x => x.UpdatedBy).ToList());

        return result;
    }

    public Result AddCourse(Course course, IEnumerable<Material> materials, IEnumerable<Skill> skills)
    {
        var result = ValidateCourse(course);
        if (result.IsFailed)
        {
            return result;
        }

        return Result.Try(() =>
        {
            _courseRepository.Add(course);

            foreach (var material in materials)
            {
                var link = new CourseMaterial()
                {
                    CourseId = course.Id,
                    MaterialId = material.Id
                };

                _courseMaterialRepository.Add(link);
            }

            foreach (var skill in skills)
            {
                var link = new CourseSkill()
                {
                    CourseId = course.Id,
                    SkillId = skill.Id
                };

                _courseSkillRepository.Add(link);
            }
        });
    }

    public Result UpdateCourse(Course course, IEnumerable<Material> materials, IEnumerable<Skill> skills)
    {
        var result = ValidateCourse(course);
        if (result.IsFailed)
        {
            return result;
        }

        return Result.Try(() =>
        {
            var oldCourseMaterials = _courseMaterialRepository.FindAll(x => x.CourseId == course.Id).ToList();
            var newCourseMaterials = materials.Select(material => new CourseMaterial() { CourseId = course.Id, MaterialId = material.Id }).ToList();

            var courseMaterialComparer = new CourseMaterialComparer();
            var courseMaterialsDelete = oldCourseMaterials.Except(newCourseMaterials, courseMaterialComparer).ToList();
            var courseMaterialsAdd = newCourseMaterials.Except(oldCourseMaterials, courseMaterialComparer).ToList();

            _courseMaterialRepository.RemoveRange(courseMaterialsDelete);
            _courseMaterialRepository.AddRange(courseMaterialsAdd);

            var oldCourseSkills = _courseSkillRepository.FindAll(x => x.CourseId == course.Id).ToList();
            var newCourseSkills = skills.Select(skill => new CourseSkill() { CourseId = course.Id, SkillId = skill.Id }).ToList();

            var courseSkillComparer = new CourseSkillComparer();
            var courseSkillDelete = oldCourseSkills.Except(newCourseSkills, courseSkillComparer).ToList();
            var courseSkillsAdd = newCourseSkills.Except(oldCourseSkills, courseSkillComparer).ToList();

            _courseSkillRepository.RemoveRange(courseSkillDelete);
            _courseSkillRepository.AddRange(courseSkillsAdd);

            _courseRepository.Update(course);
        });
    }

    public Result DeleteCourse(Course course)
    {
        if (course == null)
        {
            return Result.Fail(ErrorMessages.ModelIsNull);
        }

        return Result.Try(() =>
        {
            var courseMaterials = _courseMaterialRepository.FindAll(x => x.CourseId == course.Id);
            _courseMaterialRepository.RemoveRange(courseMaterials);

            var courseSkills = _courseSkillRepository.FindAll(x => x.CourseId == course.Id);
            _courseSkillRepository.RemoveRange(courseSkills);

            _courseRepository.Remove(course);
        });
    }

    private Result ValidateCourse(Course course)
    {
        if (course == null)
        {
            return Result.Fail(ErrorMessages.ModelIsNull);
        }

        var validator = new CourseValidator();

        try
        {
            validator.ValidateAndThrow(course);
        }
        catch (ValidationException e)
        {
            return Result.Fail(new Error(ErrorMessages.ValidationError).CausedBy(e));
        }

        return Result.Ok();
    }
}