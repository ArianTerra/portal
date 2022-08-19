using EducationPortalConsole.Core.Entities.JoinEntities;

namespace EducationPortalConsole.Core.Entities.Materials;

public class BookMaterial : Material
{
    public ICollection<BookMaterialBookAuthor> BookMaterialBookAuthors { get; set; }

    public int Pages { get; set; }

    public int Year { get; set; } //TODO maybe change it to DateOnly

    public string Format { get; set; }

    // public IEnumerable<BookAuthor> Authors =>
    //     BookMaterialBookAuthors.Where(x => x.BookMaterialId == this.Id)
    //         .Select(x => x.BookAuthor);
}