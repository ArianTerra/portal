namespace EducationPortal.DataAccess.DomainModels.Progress;

public class SkillProgress
{
    public Guid SkillId { get; set; }

    public Skill Skill { get; set; }

    public Guid UserId { get; set; }

    public ApplicationUser ApplicationUser { get; set; }

    public int Level { get; set; }
}