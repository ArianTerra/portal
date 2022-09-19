using AutoMapper;
using EducationPortal.BusinessLogic.DTO;
using EducationPortal.DataAccess.DomainModels.Materials;

namespace EducationPortal.BusinessLogic;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ArticleMaterial, ArticleMaterialDto>();
        // .ForMember(dest => dest.CreatedByName, opt => opt.MapFrom(src => src.CreatedBy != null ? src.CreatedBy.Name : "nuLL"))
        // .ForMember(dest => dest.UpdatedByName, opt => opt.MapFrom(src => src.UpdatedBy != null ? src.UpdatedBy.Name : "nuLL"));
        CreateMap<ArticleMaterialDto, ArticleMaterial>();
    }
}