using EducationPortal.DataAccess.DomainModels.Materials;

namespace EducationPortal.DataAccess.DomainModels.JoinEntities;

public class BookAuthorBookMaterial
{
    public Guid BookAuthorId { get; set; }

    public BookAuthor BookAuthor { get; set; }

    public Guid BookMaterialId { get; set; }

    public BookMaterial BookMaterial { get; set; }
}