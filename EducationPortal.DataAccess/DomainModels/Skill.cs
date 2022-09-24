using EducationPortal.DataAccess.DomainModels.JoinEntities;
using EducationPortal.DataAccess.DomainModels.Progress;

namespace EducationPortal.DataAccess.DomainModels;

public class Skill : AuditedEntity
{
    public string Name { get; set; }

    public ICollection<CourseSkill> CourseSkills { get; set; }

    public ICollection<SkillProgress> SkillProgresses { get; set; }

    public override string ToString()
    {
        return Name;
    }
}