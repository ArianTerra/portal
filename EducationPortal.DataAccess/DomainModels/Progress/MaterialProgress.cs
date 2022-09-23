namespace EducationPortal.DataAccess.DomainModels.Progress;

public class MaterialProgress
{
    public Guid MaterialId { get; set; }

    public Material Material { get; set; }

    public Guid UserId { get; set; }

    public ApplicationUser ApplicationUser { get; set; }

    public int Progress { get; set; }
}