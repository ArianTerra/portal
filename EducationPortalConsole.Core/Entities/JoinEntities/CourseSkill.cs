namespace EducationPortalConsole.Core.Entities.ManyToManyTables;

public class CourseSkill
{
    public Guid CourseId { get; set; }

    public Course Course { get; set; }

    public Guid SkillId { get; set; }

    public Skill Skill { get; set; }
}