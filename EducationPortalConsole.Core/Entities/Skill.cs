using EducationPortalConsole.Core.Entities.ManyToManyTables;
using EducationPortalConsole.Core.Entities.Progress;

namespace EducationPortalConsole.Core.Entities;

public class Skill : AuditedEntity
{
    public string Description { get; set; }

    public ICollection<CourseSkill> CourseSkills { get; set; }

    public IEnumerable<SkillProgress> SkillProgresses { get; set; }
}