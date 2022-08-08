namespace EducationPortalConsole.Core.Entities;

public class Material : BaseEntity, IAuditedEntity
{
    public int? CreatedByUserId { get; set; }
    
    public DateTime? CreatedOn { get; set; }
    
    public int? UpdatedByUserId { get; set; }
    
    public DateTime? UpdatedOn { get; set; }
}