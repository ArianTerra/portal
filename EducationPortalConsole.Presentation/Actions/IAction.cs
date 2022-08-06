namespace EducationPortalConsole.Presentation.Actions;

public interface IAction
{
    string Name { get; }
    void Run();

    public string? ToString()
    {
        return Name;
    }
}