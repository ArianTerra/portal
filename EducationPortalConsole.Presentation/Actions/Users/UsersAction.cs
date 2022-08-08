using EducationPortalConsole.Presentation.Actions.BaseActions;

namespace EducationPortalConsole.Presentation.Actions.Users;

public class UsersAction : MenuAction
{
    public UsersAction()
    {
        Name = "User Menu";
        Description = "Login to your account or create new";
        Actions = new List<Action>()
        {
            new UserLoginAction(),
            new UserRegisterAction()
        };
    }
}