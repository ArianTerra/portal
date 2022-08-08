namespace EducationPortalConsole.Core.Entities;

public class Course : BaseEntity, IAuditedEntity
{
    public string Description { get; set; }
        
    public IEnumerable<Material> Materials { get; set; }
        
    public IEnumerable<Skill> Skills { get; set; }
    
    public Guid? CreatedByUserId { get; set; }
    
    public DateTime? CreatedOn { get; set; }
    
    public Guid? UpdatedByUserId { get; set; }
    
    public DateTime? UpdatedOn { get; set; }
}