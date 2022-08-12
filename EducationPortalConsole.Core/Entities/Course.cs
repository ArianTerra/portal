using System.ComponentModel.DataAnnotations.Schema;

namespace EducationPortalConsole.Core.Entities;

public class Course : BaseEntity, IAuditedEntity
{
    public Course()
    {
        Materials = new HashSet<Material>();
    }

    public string Description { get; set; }

    public IEnumerable<Material> Materials { get; set; }

    public IEnumerable<Skill> Skills { get; set; }

    public Guid? CreatedById { get; set; }

    public User? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? UpdatedById { get; set; }

    public User? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }
}