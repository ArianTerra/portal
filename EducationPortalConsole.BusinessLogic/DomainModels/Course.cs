namespace EducationPortalConsole.BusinessLogic.DomainModels;

public class Course
{
    public string Name { get; set; }
        
    public string Description { get; set; }
        
    public IEnumerable<Material> Materials { get; set; }
        
    public IEnumerable<Skill> Skills { get; set; }
}