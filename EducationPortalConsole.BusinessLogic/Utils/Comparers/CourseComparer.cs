using EducationPortalConsole.Core.Entities;

namespace EducationPortalConsole.BusinessLogic.Utils.Comparers;

public class CourseComparer : IEqualityComparer<Course>
{
    public bool Equals(Course? x, Course? y)
    {
        if (ReferenceEquals(x, y))
        {
            return true;
        }

        if (ReferenceEquals(x, null))
        {
            return false;
        }

        if (ReferenceEquals(y, null))
        {
            return false;
        }

        if (x.GetType() != y.GetType())
        {
            return false;
        }

        return x.Id == y.Id;
    }

    public int GetHashCode(Course obj)
    {
        return obj.Id.GetHashCode();
    }
}