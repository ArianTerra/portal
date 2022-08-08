using System.Diagnostics.CodeAnalysis;
using EducationPortalConsole.Core.Entities;
using EducationPortalConsole.DataAccess.Repositories;

namespace EducationPortalConsole.BusinessLogic.Services;

public class CourseService : ICourseService
{
    private readonly ICourseRepository _repository;

    public CourseService()
    {
        _repository = new CourseRepository("Courses");
    }
    
    public CourseService(ICourseRepository repository)
    {
        _repository = repository;
    }

    public Course? GetById(Guid id)
    {
        return _repository.FindFirst(x => x.Id == id);
    }

    public IEnumerable<Course> GetAll()
    {
        return _repository.GetAll();
    }

    public void Add([NotNull] Course course)
    {
        _repository.Add(course);
    }

    public void Update(Course course)
    {
        _repository.Update(course);
    }

    public bool Delete(Course course)
    {
        return _repository.Delete(course);
    }
}