using EducationPortalConsole.Core.Entities.Materials;

namespace EducationPortalConsole.Presentation.Actions.Materials.EditActions;

public class EditVideoAction : Action
{
    private VideoMaterial _videoMaterial;
    public EditVideoAction(VideoMaterial videoMaterial)
    {
        Name = "Edit Video";
        _videoMaterial = videoMaterial;
    }

    public override void Run()
    {
        base.Run();
        
        WaitForUserInput();
        Back(2);
    }
}