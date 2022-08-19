using EducationPortalConsole.BusinessLogic.Services;
using EducationPortalConsole.BusinessLogic.Services.MaterialServices;

namespace EducationPortalConsole.Presentation;

public class Configuration
{
    private static Configuration? _userSession;

    private Configuration()
    {
        UserService = new UserService();
        MaterialService = new MaterialService();
        CourseService = new CourseService();
        ArticleMaterialService = new ArticleMaterialService();
        BookMaterialService = new BookMaterialService();
        VideoMaterialService = new VideoMaterialService();
    }

    public static Configuration Instance
    {
        get { return _userSession ??= new Configuration(); }
    }

    public IUserService UserService { get; }

    public IMaterialService MaterialService { get; }

    public ICourseService CourseService { get; }

    public ArticleMaterialService ArticleMaterialService { get; }

    public BookMaterialService BookMaterialService { get; }

    public VideoMaterialService VideoMaterialService { get; }
}