using EducationPortal.BusinessLogic.DTO.Abstract;

namespace EducationPortal.BusinessLogic.DTO;

public class VideoQualityDto : BaseDto
{
    public string Name { get; set; }

    public override string ToString()
    {
        return Name;
    }
}