namespace EducationPortalConsole.Core.Entities.Materials;

public class ArticleMaterial : Material
{
    public DateOnly Date { get; set; }

    public string Source { get; set; }
}