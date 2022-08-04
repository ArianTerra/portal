using System.Linq.Expressions;
using EducationPortalConsole.Core;

namespace EducationPortalConsole.DataAccess.Repositories;

public interface IGenericRepository<TEntity> where TEntity : BaseEntity
{
    TEntity? GetFirst(Func<TEntity, bool> predicate);
    IEnumerable<TEntity> GetAll(Func<TEntity, bool> predicate);
    void Add(TEntity entity);
    void Update(TEntity entity);
    bool Delete(TEntity entity);
}