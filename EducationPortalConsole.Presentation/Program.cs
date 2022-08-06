using EducationPortalConsole.BusinessLogic.Services;
using EducationPortalConsole.DataAccess.Repositories;
using EducationPortalConsole.Presentation.Actions;
using EducationPortalConsole.Presentation.Actions.Users;

IUserService userService = new UserService(new UserRepository("Users"));

MenuAction mainMenu = new MenuAction("Menu")
{
    Actions = {new UserLoginAction("Login"), new UserRegisterAction("Registration")}
};

mainMenu.Run();