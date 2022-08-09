using EducationPortalConsole.Core.Entities.Materials;

namespace EducationPortalConsole.Presentation.Actions.Materials.EditActions;

public class EditBookAction : Action
{
    private BookMaterial _bookMaterial;
    public EditBookAction(BookMaterial bookMaterial)
    {
        Name = "Edit Book";
        _bookMaterial = bookMaterial;
    }

    public override void Run()
    {
        base.Run();
        
        WaitForUserInput();
        Back(2);
    }
}