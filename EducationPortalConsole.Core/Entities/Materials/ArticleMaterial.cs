namespace EducationPortalConsole.Core.Entities.Materials;

public class ArticleMaterial : Material
{
    public DateOnly Date { get; set; } //TODO Date is not mapped to DB wtf

    public string Source { get; set; }
}