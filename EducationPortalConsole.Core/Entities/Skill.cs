using EducationPortalConsole.Core.Entities.JoinEntities;
using EducationPortalConsole.Core.Entities.Progress;

namespace EducationPortalConsole.Core.Entities;

public class Skill : AuditedEntity
{
    public string Name { get; set; }

    public string? Description { get; set; }

    public ICollection<CourseSkill> CourseSkills { get; set; }

    public ICollection<SkillProgress> SkillProgresses { get; set; }
}