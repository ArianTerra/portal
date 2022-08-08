namespace EducationPortalConsole.Presentation.Actions;

public static class ActionNavigationProvider
{
    private static Stack<Action> _actionsQueue = new Stack<Action>();

    public static void AddAction(Action action)
    {
        if (!_actionsQueue.Contains(action))
        {
            _actionsQueue.Push(action);
        }
    }

    public static Action? GetAction()
    {
        if (_actionsQueue.Count == 0)
        {
            return null;
        }

        _actionsQueue.Pop();
        return _actionsQueue.Pop();
    }
}