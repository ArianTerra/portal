using EducationPortalConsole.Presentation.Actions.BaseActions;

namespace EducationPortalConsole.Presentation.Actions.Materials;

public class MaterialsAction : MenuAction
{
    public MaterialsAction()
    {
        Name = "Materials commands";
        Description = "Select any CRUD operation: ";
        Actions = new List<Action>()
        {
            new AddMaterialAction(),
            new ShowAllMaterialsAction(),
            new ShowMaterialInfo(),
            new ShowMaterialByIdAction(),
            new EditMaterialActions(),
            new DeleteMaterialsAction(),
            new BackAction()
        };
    }
}