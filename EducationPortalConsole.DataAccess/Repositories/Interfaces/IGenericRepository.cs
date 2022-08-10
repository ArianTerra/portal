using EducationPortalConsole.Core;

namespace EducationPortalConsole.DataAccess.Repositories;

public interface IGenericRepository<TEntity> where TEntity : BaseEntity
{
    TEntity? FindFirst(Func<TEntity, bool> predicate);

    IEnumerable<TEntity> FindAll(Func<TEntity, bool> predicate);

    IEnumerable<TEntity> GetAll();

    void Add(TEntity entity);

    void Update(TEntity entity);

    bool Delete(TEntity entity);
}