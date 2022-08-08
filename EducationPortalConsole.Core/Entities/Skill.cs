namespace EducationPortalConsole.Core.Entities;

public class Skill : BaseEntity, IAuditedEntity
{
    public string Description { get; set; }
    
    public Guid? CreatedByUserId { get; set; }
    
    public DateTime? CreatedOn { get; set; }
    
    public Guid? UpdatedByUserId { get; set; }
    
    public DateTime? UpdatedOn { get; set; }
}