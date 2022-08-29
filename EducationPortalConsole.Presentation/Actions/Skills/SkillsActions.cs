using EducationPortalConsole.Presentation.Actions.BaseActions;

namespace EducationPortalConsole.Presentation.Actions.Skills;

public class SkillsActions : MenuAction
{
    public SkillsActions()
    {
        Name = "Skills commands";
        Actions = new List<Action>()
        {
            new AddSkillAction(),
            new ShowSkillsAction(),
            new EditSkillAction(),
            new DeleteSkillsAction(),
            new BackAction()
        };
    }
}