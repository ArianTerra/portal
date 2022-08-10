namespace EducationPortalConsole.Presentation.Actions;

public static class ActionNavigationProvider
{
    private static Stack<Action> _actionsQueue = new Stack<Action>();

    public static void AddToNavigationHistory(Action action)
    {
        if (!_actionsQueue.Contains(action))
        {
            _actionsQueue.Push(action);
        }
    }

    public static Action? GetLastAction(int steps = 1)
    {
        Action result = null;

        if (_actionsQueue.Count == 0)
        {
            return result;
        }

        for (int i = 0; i <= steps; i++)
        {
            result = _actionsQueue.Pop();
        }

        return result;
    }
}