namespace EducationPortal.BusinessLogic.DTO;

public class CourseProgressDto
{
    public Guid CourseId { get; set; }

    public string CourseName { get; set; }

    public int Progress { get; set; }

    public IEnumerable<MaterialProgressDto> Materials { get; set; }
}