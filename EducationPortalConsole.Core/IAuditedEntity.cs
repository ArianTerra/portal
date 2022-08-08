using EducationPortalConsole.Core.Entities;

namespace EducationPortalConsole.Core;

public interface IAuditedEntity
{
    User? CreatedBy { get; set; }
    DateTime? CreatedOn { get; set; }
    User? UpdatedBy { get; set; }
    DateTime? UpdatedOn { get; set; }
}