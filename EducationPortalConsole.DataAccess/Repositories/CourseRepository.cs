using EducationPortalConsole.Core.Entities;

namespace EducationPortalConsole.DataAccess.Repositories;

public class CourseRepository : GenericRepository<Course>, ICourseRepository
{
    public CourseRepository(string filename) : base(filename)
    {
    }
}