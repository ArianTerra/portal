namespace EducationPortalConsole.Core.Entities;

public class Material : AuditedEntity
{
    public Course? Course { get; set; } //one to many
}