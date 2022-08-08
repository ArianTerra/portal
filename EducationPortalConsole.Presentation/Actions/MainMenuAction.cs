using EducationPortalConsole.Presentation.Actions.BaseActions;
using EducationPortalConsole.Presentation.Actions.Courses;
using EducationPortalConsole.Presentation.Actions.Materials;

namespace EducationPortalConsole.Presentation.Actions;

public class MainMenuAction : MenuAction
{
    public MainMenuAction()
    {
        Name = "Main menu";
        Actions = new List<Action>()
        {
            new MaterialsAction(),
            new CoursesAction(),
            new ExitAction()
        };
    }
}