using EducationPortalConsole.BusinessLogic.Comparers;
using EducationPortalConsole.Core.Entities;
using EducationPortalConsole.Core.Entities.JoinEntities;
using EducationPortalConsole.DataAccess.Repositories;

namespace EducationPortalConsole.BusinessLogic.Services;

public class CourseService : ICourseService
{
    private readonly IGenericRepository<Course> _repository;

    private readonly IGenericRepository<CourseMaterial> _repositoryCourseMaterial;

    public CourseService()
    {
        _repository = new GenericRepository<Course>();
        _repositoryCourseMaterial = new GenericRepository<CourseMaterial>();
    }

    public CourseService(IGenericRepository<Course> repository)
    {
        _repository = repository;
    }

    public Course? GetCourseById(Guid id)
    {
        return _repository.FindFirst(x => x.Id == id,
            false,
            x => x.CreatedBy,
            x => x.UpdatedBy,
            x => x.CourseMaterials);
    }

    public IEnumerable<Course> GetAllCourses()
    {
        return _repository.FindAll(
            _ => true,
            true,
            x => x.CreatedBy,
            x => x.UpdatedBy,
            x => x.CourseMaterials);
    }

    public void AddCourse(Course course, IEnumerable<Material> materials)
    {
        _repository.Add(course);

        foreach (var material in materials)
        {
            var link = new CourseMaterial()
            {
                CourseId = course.Id,
                MaterialId = material.Id
            };

            _repositoryCourseMaterial.Add(link);
        }
    }

    public void UpdateCourse(Course course, IEnumerable<Material> materials)
    {
        var oldLinks = _repositoryCourseMaterial.FindAll(x => x.CourseId == course.Id).ToList();
        var newLinks = materials.Select(material => new CourseMaterial() { CourseId = course.Id, MaterialId = material.Id }).ToList();

        var comparer = new CourseMaterialComparer();
        var linksToDelete = oldLinks.Except(newLinks, comparer).ToList();
        var linksToAdd = newLinks.Except(oldLinks, comparer).ToList();

        _repositoryCourseMaterial.RemoveRange(linksToDelete);
        _repositoryCourseMaterial.AddRange(linksToAdd);

        _repository.Update(course);
    }

    public bool DeleteCourse(Course course)
    {
        var linksToDelete = _repositoryCourseMaterial.FindAll(x => x.CourseId == course.Id);
        _repositoryCourseMaterial.RemoveRange(linksToDelete);

        return _repository.Remove(course);
    }
}