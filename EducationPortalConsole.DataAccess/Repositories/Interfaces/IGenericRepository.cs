using System.Linq.Expressions;
using EducationPortalConsole.Core;

namespace EducationPortalConsole.DataAccess.Repositories;

public interface IGenericRepository<TEntity> where TEntity : BaseEntity
{
    TEntity? FindFirst(Expression<Func<TEntity, bool>> expression);

    IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> expression);

    IEnumerable<TEntity> GetAll();

    void Add(TEntity entity);

    void Update(TEntity entity);

    bool Delete(TEntity entity);
}