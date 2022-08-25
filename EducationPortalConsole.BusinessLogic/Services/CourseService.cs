using System.Diagnostics.CodeAnalysis;
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

    public Course? GetById(Guid id)
    {
        return _repository.FindFirst(x => x.Id == id,
            x => x.CreatedBy,
            x => x.UpdatedBy,
            x => x.CourseMaterials);
    }

    public IEnumerable<Course> GetAll()
    {
        return _repository.FindAll(
            _ => true,
            x => x.CreatedBy,
            x => x.UpdatedBy,
            x => x.CourseMaterials);
    }

    public void Add(Course course, IEnumerable<Material> materials)
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

    public void Update(Course course, IEnumerable<Material> materials)
    {
        _repository.Update(course);

        var linksToDelete = _repositoryCourseMaterial.FindAll(x => x.CourseId == course.Id);
        foreach (var item in linksToDelete)
        {
            _repositoryCourseMaterial.Delete(item);
        }

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

    public bool Delete(Course course)
    {
        var linksToDelete = _repositoryCourseMaterial.FindAll(x => x.CourseId == course.Id);
        foreach (var item in linksToDelete)
        {
            _repositoryCourseMaterial.Delete(item);
        }

        return _repository.Delete(course);
    }
}