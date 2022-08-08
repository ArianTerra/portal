namespace EducationPortalConsole.Core.Entities;

public class Skill : BaseEntity, IAuditedEntity
{
    public string Description { get; set; }
    
    public int? CreatedByUserId { get; set; }
    
    public DateTime? CreatedOn { get; set; }
    
    public int? UpdatedByUserId { get; set; }
    
    public DateTime? UpdatedOn { get; set; }
}