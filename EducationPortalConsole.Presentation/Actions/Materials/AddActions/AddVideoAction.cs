using EducationPortalConsole.BusinessLogic.Services;

namespace EducationPortalConsole.Presentation.Actions.Materials.AddActions;

public class AddVideoAction : Action
{
    public AddVideoAction()
    {
        Name = "Add Video";
    }
    public override void Run()
    {
        base.Run();

        IMaterialService materialService = new MaterialService();

        //TODO
        Console.WriteLine("NOT IMPLEMENTED");
            
        WaitForUserInput();
        Back();
    }
}