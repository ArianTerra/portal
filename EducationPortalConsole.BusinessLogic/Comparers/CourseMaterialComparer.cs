using EducationPortalConsole.Core.Entities.JoinEntities;

namespace EducationPortalConsole.BusinessLogic.Comparers;

public class CourseMaterialComparer : IEqualityComparer<CourseMaterial>
{
    public bool Equals(CourseMaterial x, CourseMaterial y)
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

        return x.CourseId.Equals(y.CourseId) && x.MaterialId.Equals(y.MaterialId);
    }

    public int GetHashCode(CourseMaterial obj)
    {
        return HashCode.Combine(obj.CourseId, obj.MaterialId);
    }
}