using EducationPortalConsole.Core.Entities;

namespace EducationPortalConsole.Core;

public interface IAuditedEntity
{
    Guid? CreatedByUserId { get; set; }
    DateTime? CreatedOn { get; set; }
    Guid? UpdatedByUserId { get; set; }
    DateTime? UpdatedOn { get; set; }
}