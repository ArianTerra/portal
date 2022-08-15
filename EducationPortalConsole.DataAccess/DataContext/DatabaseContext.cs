using System.Data.Entity;
using EducationPortalConsole.Core.Entities;

namespace EducationPortalConsole.DataAccess.DataContext;

public class DatabaseContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public DbSet<Course> Courses { get; set; }

    public DbSet<Material> Materials { get; set; }
}