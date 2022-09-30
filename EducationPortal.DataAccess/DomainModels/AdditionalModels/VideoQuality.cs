using EducationPortal.DataAccess.DomainModels.Materials;

namespace EducationPortal.DataAccess.DomainModels.AdditionalModels;

public class VideoQuality : BaseEntity
{
    public string Name { get; set; }

    public ICollection<VideoMaterial> VideoMaterials { get; set; }
}