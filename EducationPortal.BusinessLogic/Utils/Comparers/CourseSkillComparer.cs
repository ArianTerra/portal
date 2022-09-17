using EducationPortal.DataAccess.DomainModels.JoinEntities;

namespace EducationPortal.BusinessLogic.Utils.Comparers;

public class CourseSkillComparer : IEqualityComparer<CourseSkill>
{
    public bool Equals(CourseSkill? x, CourseSkill? y)
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

        return x.CourseId.Equals(y.CourseId) && x.SkillId.Equals(y.SkillId);
    }

    public int GetHashCode(CourseSkill obj)
    {
        return HashCode.Combine(obj.CourseId, obj.SkillId);
    }
}