namespace EducationPortalConsole.Core.Entities;

public class Material : BaseEntity, IAuditedEntity
{
    public User? CreatedBy { get; set; }

    public Guid? CreatedById { get; set; }

    public DateTime? CreatedOn { get; set; }

    public User? UpdatedBy { get; set; }

    public Guid? UpdatedById { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Course? Course { get; set; } //one to many
}