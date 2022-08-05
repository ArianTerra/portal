using EducationPortalConsole.Core.Entities;

namespace EducationPortalConsole.BusinessLogic.Services;

public interface ICourseService
{
    Course? GetById(int id);

    IEnumerable<Course> GetAll();

    void Add(Course course);

    void Update(Course course);

    bool Delete(Course course);
}