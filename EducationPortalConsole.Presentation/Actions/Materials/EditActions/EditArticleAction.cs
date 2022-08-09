using EducationPortalConsole.Core.Entities.Materials;

namespace EducationPortalConsole.Presentation.Actions.Materials.EditActions;

public class EditArticleAction : Action
{
    private ArticleMaterial _articleMaterial;
    public EditArticleAction(ArticleMaterial articleMaterial)
    {
        Name = "Edit Article";
        _articleMaterial = articleMaterial;
    }

    public override void Run()
    {
        base.Run();
        
        WaitForUserInput();
        Back(2);
    }
}