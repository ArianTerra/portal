using EducationPortalConsole.Core.Entities.Materials;

namespace EducationPortalConsole.Core.Entities;

public class BookAuthor : BaseEntity
{
    public string Name { get; set; }

    public ICollection<BookMaterial> Books { get; set; }

    public override string ToString()
    {
        return Name;
    }
}