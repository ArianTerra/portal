namespace EducationPortal.DataAccess.DomainModels;

public class AuditedEntity : BaseEntity
{
    public Guid? CreatedById { get; set; }

    public ApplicationUser? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? UpdatedById { get; set; }

    public ApplicationUser? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }
}