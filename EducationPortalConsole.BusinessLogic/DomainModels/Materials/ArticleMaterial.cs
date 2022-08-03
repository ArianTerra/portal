namespace EducationPortalConsole.BusinessLogic.DomainModels.Materials;

public class ArticleMaterial : Material
{
    public DateOnly Date { get; set; }
        
    public string Source { get; set; }
}