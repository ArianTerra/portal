namespace EducationPortalConsole.Core.Entities;

public class Course : BaseEntity
{
    public string Description { get; set; }
        
    public IEnumerable<Material> Materials { get; set; }
        
    public IEnumerable<Skill> Skills { get; set; }
}