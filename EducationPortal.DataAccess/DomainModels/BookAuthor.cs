using EducationPortal.DataAccess.DomainModels.JoinEntities;

namespace EducationPortal.DataAccess.DomainModels;

public class BookAuthor : AuditedEntity
{
    public string Name { get; set; }

    public ICollection<BookAuthorBookMaterial> BookAuthorBookMaterial { get; set; }

    public override string ToString()
    {
        return Name;
    }
}