namespace EducationPortal.DataAccess.DomainModels.Progress;

public class SkillProgress
{
    public Guid SkillId { get; set; }

    public Skill Skill { get; set; }

    public Guid UserId { get; set; }

    public User User { get; set; }

    public int Level { get; set; }
}