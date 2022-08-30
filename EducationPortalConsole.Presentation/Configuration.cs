using EducationPortalConsole.BusinessLogic.Services;
using EducationPortalConsole.BusinessLogic.Services.MaterialServices;

namespace EducationPortalConsole.Presentation;

public class Configuration
{
    private static Configuration? _configuration;

    public static Configuration Instance
    {
        get { return _configuration ??= new Configuration(); }
    }

    public UserService UserService
    {
        get => new UserService();
    }

    public MaterialService MaterialService
    {
        get => new MaterialService();
    }

    public CourseService CourseService
    {
        get => new CourseService();
    }

    public ArticleMaterialService ArticleMaterialService
    {
        get => new ArticleMaterialService();
    }

    public BookMaterialService BookMaterialService
    {
        get => new BookMaterialService();
    }

    public VideoMaterialService VideoMaterialService
    {
        get => new VideoMaterialService();
    }

    public BookAuthorService BookAuthorService
    {
        get => new BookAuthorService();
    }

    public SkillService SkillService
    {
        get => new SkillService();
    }
}