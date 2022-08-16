namespace EducationPortalConsole.Core.Entities;

public class Material : AuditedEntity
{
    public Guid? CourseId { get; set; }

    public Course? Course { get; set; } //one to many
}