using EducationPortalConsole.Presentation.Actions.BaseActions;
using EducationPortalConsole.Presentation.Actions.Courses;
using EducationPortalConsole.Presentation.Actions.Materials;
using EducationPortalConsole.Presentation.Session;

namespace EducationPortalConsole.Presentation.Actions;

public class MainMenuAction : MenuAction
{
    public MainMenuAction()
    {
        Name = "Main menu";
        Description = $"You are logged in as [yellow]{UserSession.Instance.CurrentUser.Name}[/]";
        
        Actions = new List<Action>()
        {
            new MaterialsAction(),
            new CoursesAction(),
            new ExitAction()
        };
    }
    
    
}