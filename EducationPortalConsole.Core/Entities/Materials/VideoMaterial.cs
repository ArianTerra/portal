namespace EducationPortalConsole.Core.Entities.Materials;

public class VideoMaterial : Material
{
    public TimeSpan Duration { get; set; }

    public string Quality { get; set; } //TODO maybe change it to Enum
}