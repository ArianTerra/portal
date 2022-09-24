using EducationPortal.BusinessLogic.DTO;

namespace EducationPortal.Presentation.ViewModels;

public class VideoViewModel
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public TimeSpan Duration { get; set; }

    public string Quality { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? CreatedById { get; set; }

    public string? CreatedByName { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? UpdatedById { get; set; }

    public string? UpdatedByName { get; set; }

    public IEnumerable<VideoQualityDto> AvailableQualities { get; set; }
}