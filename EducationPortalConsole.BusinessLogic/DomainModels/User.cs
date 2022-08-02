using EducationPortalConsole.BusinessLogic.DomainModels.Progress;

namespace EducationPortalConsole.BusinessLogic.DomainModels;

public class User
{
    public string Name { get; set; }
        
    public IEnumerable<CourseProgress> CoursesProgress { get; set; }
    
    public IEnumerable<MaterialProgress> MaterialsProgress { get; set; }
    
    public IEnumerable<SkillProgress> SkillsProgress { get; set; }
}