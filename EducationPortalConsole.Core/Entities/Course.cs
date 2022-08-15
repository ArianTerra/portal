using System.ComponentModel.DataAnnotations.Schema;

namespace EducationPortalConsole.Core.Entities;

public class Course : AuditedEntity
{
    public Course()
    {
        Materials = new HashSet<Material>();
    }

    public string Description { get; set; }

    public virtual IEnumerable<Material> Materials { get; set; }

    public IEnumerable<Skill> Skills { get; set; }
}