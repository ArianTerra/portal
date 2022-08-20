using System.Linq.Expressions;
using EducationPortalConsole.Core;
using EducationPortalConsole.DataAccess.DataContext;
using Microsoft.EntityFrameworkCore;

namespace EducationPortalConsole.DataAccess.Repositories;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
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

        if (includeParams.Any())
        {
            foreach (var param in includeParams)
            {
                query = query.Include(param);
            }
        }

        return query.AsNoTracking().FirstOrDefault();
    }

    public IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> expression,
        params Expression<Func<TEntity, object>>[] includeParams)
    {
        var query = _context.Set<TEntity>().Where(expression);

        if (includeParams.Any())
        {
            foreach (var param in includeParams)
            {
                query = query.Include(param);
            }
        }

        return query.AsNoTracking();
    }

    public IEnumerable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] includeParams)
    {
        var query = _context.Set<TEntity>().AsNoTracking();

        if (includeParams.Any())
        {
            foreach (var param in includeParams)
            {
                query = query.Include(param);
            }
        }

        return query;
    }

    public void Add(TEntity entity)
    {
        _context.Set<TEntity>().Add(entity);
        Save();
    }

    public void Update(TEntity entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        Save();
    }

    public bool Delete(TEntity entity)
    {
        var result = entity == _context.Set<TEntity>().Remove(entity).Entity;
        Save();
        return result;
    }

    public void DeleteRange(IEnumerable<TEntity> entities)
    {
        foreach (var entity in entities)
        {
            _context.Set<TEntity>().Remove(entity);
        }
    }

    public void Save()
    {
        _context.SaveChanges();
    }
}