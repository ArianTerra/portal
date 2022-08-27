using EducationPortalConsole.Core.Entities;

namespace EducationPortalConsole.Core;

public class AuditedEntity : BaseEntity
{
    public Guid? CreatedById { get; set; }

    public User? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? UpdatedById { get; set; }

    public User? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }
}