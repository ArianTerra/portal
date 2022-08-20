using EducationPortalConsole.Core.Entities.JoinEntities;

namespace EducationPortalConsole.Core.Entities.Materials;

public class BookAuthor : AuditedEntity
{
    public string Name { get; set; }

    public ICollection<BookAuthorBookMaterial> BookAuthorBookMaterial { get; set; }

    // public IEnumerable<BookMaterial> GetBooks()
    // {
    //     if (BookAuthorBookMaterial == null)
    //     {
    //         return new List<BookMaterial>();
    //     }
    //
    //     return BookAuthorBookMaterial
    //         .Where(x => x.BookAuthorId == this.Id).Select(x => x.BookMaterial);
    // }

    public override string ToString()
    {
        return Name;
    }
}