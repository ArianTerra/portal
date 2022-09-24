using EducationPortal.BusinessLogic.DTO;
using EducationPortal.BusinessLogic.Services;
using EducationPortal.BusinessLogic.Services.Interfaces;
using EducationPortal.BusinessLogic.Validators.FluentValidation;
using EducationPortal.DataAccess;
using EducationPortal.DataAccess.DomainModels;
using EducationPortal.DataAccess.DomainModels.AdditionalModels;
using EducationPortal.DataAccess.DomainModels.Materials;
using EducationPortal.DataAccess.Repositories;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using ConfigurationManager = Microsoft.Extensions.Configuration.ConfigurationManager;

namespace EducationPortal.Presentation;

public static class CustomServiceExtension
{
    public static void AddCustomServices(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddAutoMapper(typeof(BusinessLogic.MappingProfile));
        services.AddAutoMapper(typeof(Presentation.MappingProfile));

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        services.AddIdentity<ApplicationUser, ApplicationRole>()
            .AddEntityFrameworkStores<DatabaseContext>()
            .AddDefaultTokenProviders();

        services.Configure<IdentityOptions>(options =>
        {
            // Password settings.
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredLength = int.Parse(configuration["User:Password:MinSize"]);
        });

        services.ConfigureApplicationCookie(options =>
        {
            // Cookie settings
            options.Cookie.HttpOnly = true;
            options.ExpireTimeSpan = TimeSpan.FromHours(1);

            options.LoginPath = "/Account/Login";
            //options.AccessDeniedPath = "/Identity/Account/AccessDenied";
            options.SlidingExpiration = true;
        });

        //user
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IValidator<UserRegisterDto>, UserRegisterDtoValidator>();
        services.AddScoped<IValidator<UserLoginDto>, UserLoginDtoValidator>();

        //materials
        services.AddScoped<IMaterialService, MaterialService>();

        services.AddScoped<IValidator<ArticleMaterial>, ArticleMaterialValidator>();
        services.AddScoped<IArticleMaterialService, ArticleMaterialService>();

        services.AddScoped<IValidator<VideoMaterial>, VideoMaterialValidator>();
        services.AddScoped<IVideoMaterialService, VideoMaterialService>();

        services.AddScoped<IValidator<BookMaterial>, BookMaterialValidator>();
        services.AddScoped<IBookMaterialService, BookMaterialService>();

        //skill
        services.AddScoped<IValidator<Skill>, SkillValidator>();
        services.AddScoped<ISkillService, SkillService>();

        //course
        services.AddScoped<IValidator<Course>, CourseValidator>();
        services.AddScoped<ICourseService, CourseService>();

        //additional entities
        services.AddScoped<IValidator<VideoQuality>, VideoQualityValidator>();
        services.AddScoped<IVideoQualityService, VideoQualityService>();

        services.AddScoped<IValidator<BookAuthor>, BookAuthorValidator>();
        services.AddScoped<IBookAuthorService, BookAuthorService>();

        services.AddScoped<IValidator<BookFormatDto>, BookFormatDtoValidator>();
        services.AddScoped<IBookFormatService, BookFormatService>();
    }
}