using EducationPortalConsole.Core.Entities.JoinEntities;
using EducationPortalConsole.Core.Entities.Materials;

namespace EducationPortalConsole.Core.Entities;

public class BookAuthor : BaseEntity
{
    public string Name { get; set; }

    public ICollection<BookMaterialBookAuthor> BookMaterialBookAuthors { get; set; }

    public override string ToString()
    {
        return Name;
    }
}