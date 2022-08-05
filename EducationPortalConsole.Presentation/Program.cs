using EducationPortalConsole.Core.Entities;
using EducationPortalConsole.DataAccess.Repositories;

IUserRepository repository = new UserRepository("Users");

User user0 = new User()
{
    Id = 0,
    Name = "lambda"
};

repository.Add(user0);

User user1 = new User()
{
    Id = 1,
    Name = "epsilon"
};

repository.Add(user1);

User user2 = new User()
{
    Id = 2,
    Name = "gamma"
};

repository.Add(user2);

foreach (var user in repository.FindAll(_ => true))
{
    Console.WriteLine(user.Name);
}