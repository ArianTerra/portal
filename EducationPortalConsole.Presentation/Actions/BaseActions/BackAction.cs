namespace EducationPortalConsole.Presentation.Actions.BaseActions;

public class BackAction : Action
{
    public BackAction()
    {
        Name = "Back";
    }

    public override void Run()
    {
        //base.Run();
        Back();
    }
}