namespace EducationPortalConsole.Core.Entities;

public class Skill : BaseEntity, IAuditedEntity
{
    public string Description { get; set; }

    public User? CreatedBy { get; set; }

    public Guid? CreatedById { get; set; }

    public DateTime? CreatedOn { get; set; }

    public User? UpdatedBy { get; set; }

    public Guid? UpdatedById { get; set; }

    public DateTime? UpdatedOn { get; set; }
}