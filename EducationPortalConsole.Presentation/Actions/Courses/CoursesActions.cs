using EducationPortalConsole.Presentation.Actions.BaseActions;

namespace EducationPortalConsole.Presentation.Actions.Courses;

public class CoursesActions : MenuAction
{
    public CoursesActions()
    {
        Name = "Courses commands";
        Actions = new List<Action>()
        {
            new AddCourseAction(),
            new ShowCoursesAction(),
            new ShowCourseInfo(),
            new EditCourseAction(),
            new DeleteCoursesAction(),
            new BackAction()
        };
    }
}