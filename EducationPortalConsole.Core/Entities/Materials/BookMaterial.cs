using EducationPortalConsole.Core.Entities.JoinEntities;

namespace EducationPortalConsole.Core.Entities.Materials;

public class BookMaterial : Material
{
    public ICollection<BookAuthorBookMaterial> BookAuthorBookMaterial { get; set; }

    public int Pages { get; set; }

    public int Year { get; set; } //TODO maybe change it to DateTime

    public string Format { get; set; } //TODO change to enum
}