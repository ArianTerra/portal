using EducationPortalConsole.Core.Entities;

namespace EducationPortalConsole.Core;

public interface IAuditedEntity
{
    int? CreatedByUserId { get; set; }
    DateTime? CreatedOn { get; set; }
    int? UpdatedByUserId { get; set; }
    DateTime? UpdatedOn { get; set; }
}