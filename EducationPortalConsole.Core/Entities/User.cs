using EducationPortalConsole.Core.Entities.Progress;

namespace EducationPortalConsole.Core.Entities;

public class User : BaseEntity //TODO should it be IAuditedEntity or not??
{
    public string PasswordHash { get; set; }
    
    public string PasswordHashSalt { get; set; }
    
    public IEnumerable<CourseProgress> CoursesProgress { get; set; } //TODO this is for future features
    
    public IEnumerable<MaterialProgress> MaterialsProgress { get; set; }
    
    public IEnumerable<SkillProgress> SkillsProgress { get; set; }
}