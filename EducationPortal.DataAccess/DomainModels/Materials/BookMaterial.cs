using EducationPortal.DataAccess.DomainModels.AdditionalModels;
using EducationPortal.DataAccess.DomainModels.JoinEntities;

namespace EducationPortal.DataAccess.DomainModels.Materials;

public class BookMaterial : Material
{
    public ICollection<BookAuthorBookMaterial> BookAuthorBookMaterial { get; set; }

    public int Pages { get; set; }

    public int Year { get; set; }

    public Guid BookFormatId { get; set; }

    public BookFormat BookFormat { get; set; }
}