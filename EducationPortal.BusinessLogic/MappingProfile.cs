using AutoMapper;
using EducationPortal.BusinessLogic.DTO;
using EducationPortal.DataAccess.DomainModels.Materials;

namespace EducationPortal.BusinessLogic;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ArticleMaterial, ArticleMaterialDto>();
    }
}