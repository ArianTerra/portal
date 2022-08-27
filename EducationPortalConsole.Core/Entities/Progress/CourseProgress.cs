namespace EducationPortalConsole.Core.Entities.Progress;

public class CourseProgress : BaseEntity
{
    public Guid CourseId { get; set; }

    public Course Course { get; set; }

    public Guid UserId { get; set; }

    public User User { get; set; }

    public int Progress { get; set; }
}