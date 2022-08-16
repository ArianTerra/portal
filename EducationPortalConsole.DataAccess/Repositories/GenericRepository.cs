using System.Data.Entity;
using System.Linq.Expressions;
using EducationPortalConsole.Core;
using EducationPortalConsole.DataAccess.DataContext;

namespace EducationPortalConsole.DataAccess.Repositories;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
{
    private readonly DatabaseContext _context;

    public GenericRepository()
    {
        _context = new DatabaseContext(); //TODO should it be initialized here?
    }

    public TEntity? FindFirst(Expression<Func<TEntity, bool>> expression,
        params Expression<Func<TEntity, object>>[] includeParams)
    {
        var query = _context.Set<TEntity>().Where(expression);

        if (includeParams != null && includeParams.Any())
        {
            foreach (var param in includeParams)
            {
                query.Include(param);
            }
        }

        return query.FirstOrDefault();
    }

    public IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> expression,
        params Expression<Func<TEntity, object>>[] includeParams)
    {
        var query = _context.Set<TEntity>().Where(expression);

        if (includeParams != null && includeParams.Any())
        {
            foreach (var param in includeParams)
            {
                query.Include(param);
            }
        }

        return query;
    }

    public IQueryable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] includeParams)
    {
        var query = _context.Set<TEntity>();

        if (includeParams != null && includeParams.Any())
        {
            foreach (var param in includeParams)
            {
                query.Include(param);
            }
        }

        return query;
    }

    public void Add(TEntity entity)
    {
        _context.Set<TEntity>().Add(entity);
        _context.SaveChanges();
    }

    public void Update(TEntity entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        _context.SaveChanges();
    }

    public bool Delete(TEntity entity)
    {
        var result = entity == _context.Set<TEntity>().Remove(entity);
        _context.SaveChanges();
        return result;
    }
}