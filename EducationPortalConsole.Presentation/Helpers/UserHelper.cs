using EducationPortalConsole.BusinessLogic.Services;
using EducationPortalConsole.Core.Entities;

namespace EducationPortalConsole.Presentation.Helpers;

public static class UserHelper
{
    public static string GetUsernameById(int? id)
    {
        if (id == null)
        {
            return String.Empty;
        }

        User? user = Configuration.Instance.UserService.GetById((int)id);
        if (user == null)
        {
            throw new ArgumentException($"User with ID {id} not found");
        }

        return user.Name;
    }
}