using System.Linq.Expressions;
using EducationPortalConsole.Core;

namespace EducationPortalConsole.DataAccess.Repositories;

public interface IGenericRepository<TEntity> where TEntity : class
{
    TEntity? FindFirst(Expression<Func<TEntity, bool>> expression,
        bool tracking = false,
        params Expression<Func<TEntity, object>>[] includeParams);

    IQueryable<TEntity> FindAll(Expression<Func<TEntity, bool>> expression,
        bool tracking = false,
        params Expression<Func<TEntity, object>>[] includeParams);

    void Add(TEntity entity);

    void Update(TEntity entity);

    bool Remove(TEntity entity);

    void RemoveRange(IEnumerable<TEntity> entities);
}