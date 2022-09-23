namespace EducationPortal.DataAccess.DomainModels.Progress;

public class CourseProgress
{
    public Guid CourseId { get; set; }

    public Course Course { get; set; }

    public Guid UserId { get; set; }

    public ApplicationUser ApplicationUser { get; set; }

    public int Progress { get; set; }
}