using EducationPortalConsole.Core.Entities;

namespace EducationPortalConsole.Presentation.Session;

public class UserSession
{
    private static UserSession? _userSession;
    
    public static UserSession Instance
    {
        get { return _userSession ??= new UserSession(); }
    }
    
    public User? CurrentUser { get; set; }
}