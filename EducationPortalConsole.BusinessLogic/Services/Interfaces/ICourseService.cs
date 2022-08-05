using EducationPortalConsole.Core.Entities;

namespace EducationPortalConsole.BusinessLogic.Services;

public interface ICourseService
{
    Course? GetFirst(Func<Course, bool> predicate);

    IEnumerable<Course> FindAll(Func<Course, bool> predicate);

    IEnumerable<Course> GetAll();

    void Add(Course entity);

    void Update(Course entity);

    bool Delete(Course entity);
}