using AutoMapper;
using EducationPortal.BusinessLogic.DTO;
using EducationPortal.Presentation.ViewModels;

namespace EducationPortal.Presentation;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<VideoMaterialDto, VideoViewModel>()
            .ForMember(dest => dest.Quality, opt => opt.MapFrom(x => x.Quality.Name));
        CreateMap<VideoViewModel, VideoMaterialDto>()
            .ForMember(dest => dest.Quality, opt => opt.Ignore());

        CreateMap<CourseViewModel, CourseDto>();
    }
}