using EducationPortal.BusinessLogic.DTO.Abstract;

namespace EducationPortal.BusinessLogic.DTO;

public class VideoMaterialDto : AuditedDto
{
    public string Name { get; set; }

    public TimeSpan Duration { get; set; }

    public VideoQualityDto Quality { get; set; }
}