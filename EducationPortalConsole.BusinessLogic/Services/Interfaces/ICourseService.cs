using EducationPortalConsole.Core.Entities;

namespace EducationPortalConsole.BusinessLogic.Services;

public interface ICourseService
{
    Course? GetCourseById(Guid id);

    IEnumerable<Course> GetAllCourses();

    void AddCourse(Course course, IEnumerable<Material> materials);

    void UpdateCourse(Course course, IEnumerable<Material> materials);

    bool DeleteCourse(Course course);
}