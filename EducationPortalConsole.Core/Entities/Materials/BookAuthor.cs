using EducationPortalConsole.Core.Entities.JoinEntities;

namespace EducationPortalConsole.Core.Entities.Materials;

public class BookAuthor : AuditedEntity
{
    public string Name { get; set; }

    public ICollection<BookAuthorBookMaterial> BookAuthorBookMaterial { get; set; }

    public override string ToString()
    {
        return Name;
    }
}