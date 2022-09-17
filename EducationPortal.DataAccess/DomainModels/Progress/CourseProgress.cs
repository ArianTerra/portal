namespace EducationPortal.DataAccess.DomainModels.Progress;

public class CourseProgress
{
    public Guid CourseId { get; set; }

    public Course Course { get; set; }

    public Guid UserId { get; set; }

    public User User { get; set; }

    public int Progress { get; set; }
}