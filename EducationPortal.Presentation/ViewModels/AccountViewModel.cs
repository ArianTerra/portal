using EducationPortal.BusinessLogic.DTO;

namespace EducationPortal.Presentation.ViewModels;

public class AccountViewModel
{
    public UserAccountDto UserAccountDto { get; set; }

    public IEnumerable<CourseProgressDto> CoursesProgress { get; set; }

    public IEnumerable<SkillProgressDto> SkillProgress { get; set; }
}