using EducationPortal.BusinessLogic.DTO;

namespace EducationPortal.Presentation.ViewModels;

public class CourseViewModel
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public IEnumerable<MaterialDto> AvailableMaterials { get; set; }

    public IEnumerable<MaterialDto> Materials { get; set; }

    public IEnumerable<string> SelectedMaterials { get; set; } = new List<string>();

    public IEnumerable<SkillDto> AvailableSkills { get; set; }

    public IEnumerable<string> SelectedSkills { get; set; } = new List<string>();
}