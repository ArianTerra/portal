namespace EducationPortalConsole.Core.Entities.JoinEntities;

public class CourseSkill
{
    public Guid CourseId { get; set; }

    public Course Course { get; set; }

    public Guid SkillId { get; set; }

    public Skill Skill { get; set; }
}