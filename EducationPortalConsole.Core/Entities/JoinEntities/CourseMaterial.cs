namespace EducationPortalConsole.Core.Entities.ManyToManyTables;

public class CourseMaterial : BaseEntity
{
    public Guid CourseId { get; set; }

    public Course Course { get; set; }

    public Guid MaterialId { get; set; }

    public Material Material { get; set; }
}