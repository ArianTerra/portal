using EducationPortalConsole.Presentation.Actions.BaseActions;
using EducationPortalConsole.Presentation.Actions.Materials.BookAuthorActions;

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
            new EditMaterialActions(),
            new DeleteMaterialsAction(),
            new AuthorActions(),
            new BackAction()
        };
    }
}