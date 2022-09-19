using EducationPortal.BusinessLogic.Services.Interfaces;
using EducationPortal.BusinessLogic.Services.MaterialServices;
using EducationPortal.BusinessLogic.Validators.FluentValidation;
using EducationPortal.DataAccess.DomainModels.Materials;
using EducationPortal.DataAccess.Repositories;
using FluentValidation;
using Task1.DataAccess.Repository;

namespace EducationPortal.Presentation;

public static class CustomServiceExtension
{
    public static void AddCustomServices(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(BusinessLogic.MappingProfile));

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IValidator<ArticleMaterial>, ArticleMaterialValidator>();
        services.AddScoped<IArticleMaterialService, ArticleMaterialService>();
    }
}