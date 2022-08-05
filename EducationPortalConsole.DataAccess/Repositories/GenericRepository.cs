using System.Diagnostics.CodeAnalysis;
using EducationPortalConsole.Core;
using EducationPortalConsole.DataAccess.Serializers;

namespace EducationPortalConsole.DataAccess.Repositories;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
{
    private IFileSerializer<TEntity> _fileSerializer;

    public GenericRepository(string filename)
    {
        _fileSerializer = new JsonSerializer<TEntity>(filename);
        _fileSerializer.Load();
    }
        
    public TEntity? GetFirst(Func<TEntity, bool> predicate)
    {
        return _fileSerializer.GetFirst(predicate);
    }

    public IEnumerable<TEntity> FindAll(Func<TEntity, bool> predicate)
    {
        return _fileSerializer.FindAll(predicate);
    }

    public IEnumerable<TEntity> GetAll()
    {
        return _fileSerializer.GetAll();
    }

    public void Add([NotNull] TEntity entity)
    {
        if (_fileSerializer.GetFirst(x => x.Id == entity.Id) != null)
        {
            throw new ArgumentException($"Entity with ID {entity.Id} already exist");
        }
            
        _fileSerializer.Add(entity);
        _fileSerializer.Save();
    }

    public void Update(TEntity entity)
    {
        var entityToUpdate = _fileSerializer.GetFirst(x => x.Id == entity.Id);
        if (entityToUpdate == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        _fileSerializer.Delete(entityToUpdate);
        _fileSerializer.Add(entity);
        _fileSerializer.Save();
    }

    public bool Delete(TEntity entity)
    {
        var result = _fileSerializer.Delete(entity);
        _fileSerializer.Save();
        return result;
    }
}