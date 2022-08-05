using EducationPortalConsole.BusinessLogic.Services;
using EducationPortalConsole.DataAccess.Repositories;

IUserService userService = new UserService(new UserRepository("Users"));

foreach (var user in userService.GetAll())
{
    Console.WriteLine(user.Name);
}