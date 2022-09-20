using EducationPortal.DataAccess.DomainModels.AdditionalModels;

namespace EducationPortal.DataAccess.DomainModels.Materials;

public class VideoMaterial : Material
{
    public TimeSpan Duration { get; set; }

    public Guid QualityId { get; set; }
    public VideoQuality Quality { get; set; }
}