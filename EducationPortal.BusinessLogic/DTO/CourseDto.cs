using EducationPortal.BusinessLogic.DTO.Abstract;

namespace EducationPortal.BusinessLogic.DTO;

public class CourseDto : AuditedDto
{
    public string Name { get; set; }

    public string Description { get; set; }

    public IEnumerable<SkillDto> Skills { get; set; }

    public IEnumerable<MaterialDto> Materials { get; set; }
}