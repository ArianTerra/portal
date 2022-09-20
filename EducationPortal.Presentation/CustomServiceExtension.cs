using EducationPortal.BusinessLogic.Services;
using EducationPortal.BusinessLogic.Services.Interfaces;
using EducationPortal.BusinessLogic.Validators.FluentValidation;
using EducationPortal.DataAccess.DomainModels;
using EducationPortal.DataAccess.DomainModels.AdditionalModels;
using EducationPortal.DataAccess.DomainModels.Materials;
using EducationPortal.DataAccess.Repositories;
using FluentValidation;

namespace EducationPortal.Presentation;

public static class CustomServiceExtension
{
    public static void AddCustomServices(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(BusinessLogic.MappingProfile));

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        //materials
        services.AddScoped<IValidator<ArticleMaterial>, ArticleMaterialValidator>();
        services.AddScoped<IArticleMaterialService, ArticleMaterialService>();

        services.AddScoped<IValidator<VideoMaterial>, VideoMaterialValidator>();
        services.AddScoped<IVideoMaterialService, VideoMaterialService>();

        //additional entities
        services.AddScoped<IValidator<VideoQuality>, VideoQualityValidator>();
        services.AddScoped<IVideoQualityService, VideoQualityService>();
    }
}