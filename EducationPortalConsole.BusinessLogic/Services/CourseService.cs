using EducationPortalConsole.BusinessLogic.Comparers;
using EducationPortalConsole.Core.Entities;
using EducationPortalConsole.Core.Entities.JoinEntities;
using EducationPortalConsole.DataAccess.Repositories;

namespace EducationPortalConsole.BusinessLogic.Services;

public class CourseService : ICourseService
{
    private readonly IGenericRepository<Course> _courseRepository;

    private readonly IGenericRepository<CourseMaterial> _courseMaterialRepository;

    public CourseService()
    {
        _courseRepository = new GenericRepository<Course>();
        _courseMaterialRepository = new GenericRepository<CourseMaterial>();
    }

    public CourseService(IGenericRepository<Course> courseRepository, IGenericRepository<CourseMaterial> courseMaterialRepository)
    {
        _courseRepository = courseRepository;
        _courseMaterialRepository = courseMaterialRepository;
    }

    public Course? GetCourseById(Guid id)
    {
        return _courseRepository.FindFirst(x => x.Id == id,
            false,
            x => x.CreatedBy,
            x => x.UpdatedBy,
            x => x.CourseMaterials);
    }

    public IEnumerable<Course> GetAllCourses()
    {
        return _courseRepository.FindAll(
            _ => true,
            true,
            x => x.CreatedBy,
            x => x.UpdatedBy,
            x => x.CourseMaterials);
    }

    public void AddCourse(Course course, IEnumerable<Material> materials)
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
    }

    public void UpdateCourse(Course course, IEnumerable<Material> materials)
    {
        var oldLinks = _courseMaterialRepository.FindAll(x => x.CourseId == course.Id).ToList();
        var newLinks = materials.Select(material => new CourseMaterial() { CourseId = course.Id, MaterialId = material.Id }).ToList();

        var comparer = new CourseMaterialComparer();
        var linksToDelete = oldLinks.Except(newLinks, comparer).ToList();
        var linksToAdd = newLinks.Except(oldLinks, comparer).ToList();

        _courseMaterialRepository.RemoveRange(linksToDelete);
        _courseMaterialRepository.AddRange(linksToAdd);

        _courseRepository.Update(course);
    }

    public bool DeleteCourse(Course course)
    {
        var linksToDelete = _courseMaterialRepository.FindAll(x => x.CourseId == course.Id);
        _courseMaterialRepository.RemoveRange(linksToDelete);

        return _courseRepository.Remove(course);
    }
}