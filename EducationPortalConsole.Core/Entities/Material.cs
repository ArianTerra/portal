namespace EducationPortalConsole.Core.Entities;

public class Material : BaseEntity, IAuditedEntity
{
    public Guid? CreatedByUserId { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? UpdatedByUserId { get; set; }

    public DateTime? UpdatedOn { get; set; }
}