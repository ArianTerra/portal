namespace EducationPortal.BusinessLogic.DTO;

public class VideoMaterialDto : MaterialDto
{
    public TimeSpan Duration { get; set; }

    public VideoQualityDto Quality { get; set; }
}