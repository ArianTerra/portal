using EducationPortalConsole.Core.Entities;

namespace EducationPortalConsole.Core;

public class AuditedEntity : BaseEntity
{
    public Guid? CreatedById { get; set; }

    public virtual User? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? UpdatedById { get; set; }

    public virtual User? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }
}