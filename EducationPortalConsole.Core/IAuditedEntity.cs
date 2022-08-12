using EducationPortalConsole.Core.Entities;

namespace EducationPortalConsole.Core;

public interface IAuditedEntity
{
    Guid? CreatedById { get; set; }

    User? CreatedBy { get; set; }

    DateTime? CreatedOn { get; set; }

    Guid? UpdatedById { get; set; }

    User? UpdatedBy { get; set; }

    DateTime? UpdatedOn { get; set; }
}