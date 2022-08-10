using EducationPortalConsole.Core.Entities;

namespace EducationPortalConsole.Presentation.Helpers;

public static class UserHelper
{
    public static string GetUsernameById(Guid? id)
    {
        if (id == null)
        {
            return String.Empty;
        }

        User? user = Configuration.Instance.UserService.GetById((Guid)id);
        if (user == null)
        {
            throw new ArgumentException($"User with ID {id} not found");
        }

        return user.Name;
    }
}