using EducationPortalConsole.Core.Entities.Materials;

namespace EducationPortalConsole.Core.Entities.ManyToManyTables;

public class BookMaterialBookAuthor
{
    public Guid BookMaterialId { get; set; }

    public BookMaterial BookMaterial { get; set; }

    public BookAuthor BookAuthor { get; set; }

    public Guid BookAuthorId { get; set; }
}