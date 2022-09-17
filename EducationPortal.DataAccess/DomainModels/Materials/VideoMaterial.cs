namespace EducationPortal.DataAccess.DomainModels.Materials;

public class VideoMaterial : Material
{
    public TimeSpan Duration { get; set; }

    public string Quality { get; set; } //TODO maybe change it to Enum
}