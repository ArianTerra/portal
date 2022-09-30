namespace EducationPortal.DataAccess.DomainModels.Materials;

public class ArticleMaterial : Material
{
    public DateTime PublishDate { get; set; }

    public string Source { get; set; }
}