using AutoMapper;
using EducationPortal.BusinessLogic.DTO;
using EducationPortal.DataAccess.DomainModels;
using EducationPortal.DataAccess.DomainModels.AdditionalModels;
using EducationPortal.DataAccess.DomainModels.Materials;

namespace EducationPortal.BusinessLogic;

/// <summary>
/// Mapping profile for entities between DAL and BL
/// </summary>
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ArticleMaterial, ArticleMaterialDto>();
        CreateMap<ArticleMaterialDto, ArticleMaterial>()
            .ForMember(dest => dest.CreatedOn, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedOn, opt => opt.Ignore());

        CreateMap<VideoQuality, VideoQualityDto>();
        CreateMap<VideoQualityDto, VideoQuality>();

        CreateMap<VideoMaterial, VideoMaterialDto>();
        CreateMap<VideoMaterialDto, VideoMaterial>()
            .ForMember(dest => dest.QualityId, opt => opt.MapFrom(src => src.Quality.Id))
            .ForMember(dest => dest.Quality, opt => opt.Ignore());

        CreateMap<BookAuthor, BookAuthorDto>();
        CreateMap<BookAuthorDto, BookAuthor>();

        CreateMap<BookFormat, BookFormatDto>();
        CreateMap<BookFormatDto, BookFormat>();

        CreateMap<BookMaterial, BookMaterialDto>();
        CreateMap<BookMaterialDto, BookMaterial>()
            .ForMember(dest => dest.BookFormatId, opt => opt.MapFrom(src => src.BookFormat.Id))
            .ForMember(dest => dest.BookFormat, opt => opt.Ignore());

        CreateMap<Skill, SkillDto>();
        CreateMap<SkillDto, Skill>();
    }
}