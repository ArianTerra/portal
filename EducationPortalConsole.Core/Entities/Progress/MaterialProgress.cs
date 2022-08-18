namespace EducationPortalConsole.Core.Entities.Progress;

public class MaterialProgress
{
    public Guid MaterialId { get; set; }

    public Material Material { get; set; }

    public Guid UserId { get; set; }

    public User User { get; set; }

    public int Progress { get; set; }
}