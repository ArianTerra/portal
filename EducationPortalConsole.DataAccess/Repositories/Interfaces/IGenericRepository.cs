using System.Linq.Expressions;
using EducationPortalConsole.Core;

namespace EducationPortalConsole.DataAccess.Repositories;

public interface IGenericRepository<TEntity> where TEntity : BaseEntity
{
    TEntity? FindFirst(Expression<Func<TEntity, bool>> expression,
        params Expression<Func<TEntity, object>>[] includeParams);

    IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> expression,
        params Expression<Func<TEntity, object>>[] includeParams);

    IEnumerable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] includeParams);

    void Add(TEntity entity);

    void Update(TEntity entity);

    bool Delete(TEntity entity);
}