using EducationPortal.BusinessLogic.DTO.Abstract;
using EducationPortal.DataAccess.DomainModels.Materials;

namespace EducationPortal.BusinessLogic.DTO;

public class ArticleMaterialDto : MaterialDto
{
    public DateTime PublishDate { get; set; }

    public string Source { get; set; }
}