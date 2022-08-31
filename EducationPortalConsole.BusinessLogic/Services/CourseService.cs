using EducationPortalConsole.BusinessLogic.Comparers;
using EducationPortalConsole.Core.Entities;
using EducationPortalConsole.Core.Entities.JoinEntities;
using EducationPortalConsole.DataAccess.Repositories;

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
    }

    public Course? GetCourseById(Guid id)
    {
        return _courseRepository.FindFirst(x => x.Id == id,
            true,
            x => x.CreatedBy,
            x => x.UpdatedBy,
            x => x.CourseMaterials,
            x => x.CourseSkills);
    }

    public IEnumerable<Course> GetAllCourses()
    {
        return _courseRepository.FindAll(
            _ => true, //todo remove this
            false,
            x => x.CreatedBy,
            x => x.UpdatedBy);
    }

    public void AddCourse(Course course, IEnumerable<Material> materials, IEnumerable<Skill> skills)
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
    }

    public void UpdateCourse(Course course, IEnumerable<Material> materials, IEnumerable<Skill> skills)
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
    }

    public bool DeleteCourse(Course course)
    {
        var courseMaterials = _courseMaterialRepository.FindAll(x => x.CourseId == course.Id);
        _courseMaterialRepository.RemoveRange(courseMaterials);

        var courseSkills = _courseSkillRepository.FindAll(x => x.CourseId == course.Id);
        _courseSkillRepository.RemoveRange(courseSkills);

        return _courseRepository.Remove(course);
    }
}