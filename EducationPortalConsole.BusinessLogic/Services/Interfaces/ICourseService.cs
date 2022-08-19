using EducationPortalConsole.Core.Entities;

namespace EducationPortalConsole.BusinessLogic.Services;

public interface ICourseService
{
    Course? GetById(Guid id);

    IEnumerable<Course> GetAll();

    void Add(Course course, IEnumerable<Material> materials);

    void Update(Course course, IEnumerable<Material> materials);

    bool Delete(Course course);
}