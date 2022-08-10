using EducationPortalConsole.Presentation.Actions;
using EducationPortalConsole.Presentation.Actions.Users;
using EducationPortalConsole.Presentation.Session;

while (!UserSession.Instance.IsLoggedIn)
{
    new UsersAction().Run();
}

var menu = new MainMenuAction();
menu.Run();

// ArticleMaterial material = (ArticleMaterial)Configuration.Instance.MaterialService.GetById(0);
//
// Console.WriteLine();