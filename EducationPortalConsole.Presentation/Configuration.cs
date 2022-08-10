using EducationPortalConsole.BusinessLogic.Services;

namespace EducationPortalConsole.Presentation;

public class Configuration
{
    private static Configuration? _userSession;

    private Configuration()
    {
        UserService = new UserService();
        MaterialService = new MaterialService();
        CourseService = new CourseService();
    }

    public static Configuration Instance
    {
        get { return _userSession ??= new Configuration(); }
    }

    public IUserService UserService { get; }

    public IMaterialService MaterialService { get; }

    public ICourseService CourseService { get; }
}