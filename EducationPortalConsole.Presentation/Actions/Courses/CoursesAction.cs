using EducationPortalConsole.Presentation.Actions.BaseActions;

namespace EducationPortalConsole.Presentation.Actions.Courses;

public class CoursesAction : MenuAction
{
    public CoursesAction()
    {
        Name = "Courses commands";
        Actions = new List<Action>()
        {
            new AddCourseAction(),
            new ShowCoursesAction(),
            new DeleteCoursesAction(),
            new BackAction()
        };
    }
}