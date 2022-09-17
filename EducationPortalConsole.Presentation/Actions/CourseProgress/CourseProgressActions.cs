using EducationPortalConsole.Presentation.Actions.BaseActions;

namespace EducationPortalConsole.Presentation.Actions.CourseProgress;

public class CourseProgressActions : MenuAction
{
    public CourseProgressActions()
    {
        Name = "Course Progress actions";
        Actions = new List<Action>
        {
            new ShowAvailableCoursesAction(),
            new ShowCoursesInProgressAction(),
            new ShowFinishedCoursesAction(),
            new SubscribeToCourseAction(),
            new SetCourseProgressAction(),
            new BackAction()
        };
    }
}