using System.Diagnostics.CodeAnalysis;
using EducationPortalConsole.Core.Entities;
using EducationPortalConsole.DataAccess.Repositories;

namespace EducationPortalConsole.BusinessLogic.Services;

public class CourseService : ICourseService
{
    private readonly IGenericRepository<Course> _repository;

    public CourseService()
    {
        _repository = new GenericRepository<Course>();
    }

    public CourseService(IGenericRepository<Course> repository)
    {
        _repository = repository;
    }

    public Course? GetById(Guid id)
    {
        return _repository.FindFirst(x => x.Id == id,
            x => x.CreatedBy,
            x => x.UpdatedBy);
    }

    public IEnumerable<Course> GetAll()
    {
        return _repository.GetAll(
            x => x.CreatedBy,
            x => x.UpdatedBy);
    }

    public void Add(Course course)
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