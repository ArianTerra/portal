using EducationPortalConsole.Presentation.Actions.BaseActions;
using EducationPortalConsole.Presentation.Actions.CourseProgress;
using EducationPortalConsole.Presentation.Actions.Courses;
using EducationPortalConsole.Presentation.Actions.Materials;
using EducationPortalConsole.Presentation.Actions.Skills;
using EducationPortalConsole.Presentation.Actions.Users;
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
            new CoursesActions(),
            new CourseProgressActions(),
            new SkillsActions(),
            new UserAccountInfoAction(),
            new ExitAction()
        };
    }
}