namespace EducationPortalConsole.Presentation.Actions.BaseActions;

public class ExitAction : Action
{
    public ExitAction()
    {
        Name = "Exit App";
        Description = "App closes now";
    }

    public override void Run()
    {
        base.Run();

        WaitForUserInput();
        Environment.Exit(0);
    }
}