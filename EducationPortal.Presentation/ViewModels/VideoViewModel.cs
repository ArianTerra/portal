using EducationPortal.BusinessLogic.DTO;

namespace EducationPortal.Presentation.ViewModels;

public class VideoViewModel
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public TimeSpan Duration { get; set; }

    public string Quality { get; set; }
    public IEnumerable<VideoQualityDto> AvailableQualities { get; set; }
}