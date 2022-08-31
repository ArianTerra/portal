using EducationPortalConsole.Presentation.Session;
using Spectre.Console;

namespace EducationPortalConsole.Presentation.Actions.Users;

public class UserAccountInfoAction : Action
{
    public UserAccountInfoAction()
    {
        Name = "User Account Info";
    }

    public override void Run()
    {
        base.Run();

        var user = UserSession.Instance.CurrentUser;
        var table = new Table();

        table.AddColumns("Field", "Value");
        table.AddRow("Username", user.Name);
        table.AddRow("Email", user.Email);

        AnsiConsole.Write(table);

        WaitForUserInput();
        Back();
    }
}