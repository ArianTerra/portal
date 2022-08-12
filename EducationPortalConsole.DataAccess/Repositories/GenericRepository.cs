using System.Diagnostics.CodeAnalysis;
using EducationPortalConsole.Core;
using EducationPortalConsole.DataAccess.DataContext;
using EducationPortalConsole.DataAccess.Serializers;

namespace EducationPortalConsole.DataAccess.Repositories;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
{
    //private IFileSerializer<TEntity> _fileSerializer;
    private DatabaseContext _context;

    public GenericRepository()
    {
        _context = new DatabaseContext(); //TODO should it be initialized here?
    }

    public TEntity? FindFirst(Func<TEntity, bool> predicate)
    {
        return _context.Set<TEntity>().Where(predicate).FirstOrDefault();
    }

    public IEnumerable<TEntity> FindAll(Func<TEntity, bool> predicate)
    {
        return _context.Set<TEntity>().Where(predicate);
    }

    public IEnumerable<TEntity> GetAll()
    {
        return _context.Set<TEntity>();
    }

    public void Add([NotNull] TEntity entity)
    {
        _context.Set<TEntity>().Add(entity);
        _context.SaveChanges();
    }

    public void Update(TEntity entity)
    {
        var entityToUpdate = FindFirst(x => x.Id == entity.Id);
        if (entityToUpdate == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        _context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
        _context.SaveChanges();
    }

    public bool Delete(TEntity entity)
    {
        return entity == _context.Set<TEntity>().Remove(entity);
    }
}