using EducationPortal.DataAccess.DomainModels.Materials;

namespace EducationPortal.DataAccess.DomainModels.AdditionalModels;

public class BookFormat : BaseEntity
{
    public string Name { get; set; }

    public ICollection<BookMaterial> BookMaterials { get; set; }
}