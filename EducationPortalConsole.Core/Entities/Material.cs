namespace EducationPortalConsole.Core.Entities;

public class Material : AuditedEntity
{
    public ICollection<Course> Courses { get; set; }
}