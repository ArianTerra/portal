using EducationPortalConsole.Presentation.Actions.BaseActions;
using EducationPortalConsole.Presentation.Actions.Materials.AddActions;

namespace EducationPortalConsole.Presentation.Actions.Materials;

public class AddMaterialAction : MenuAction
{
    public AddMaterialAction()
    {
        Name = "Add Material";
        Description = "Select material type:";
        Actions = new List<Action>()
        {
            new AddArticleAction(),
            new AddBookAction(),
            new AddVideoAction(),
            new BackAction()
        };
    }
}