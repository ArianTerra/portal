using System.Linq.Expressions;

namespace EducationPortalConsole.DataAccess.Serializers
{
    public interface IFileSerializer<T>
    {
        void Add(T entity);
        T? GetFirst(Func<T, bool> predicate);
        IEnumerable<T> GetAll(Func<T, bool> predicate);
        bool Delete(T entity);
        void Load();
        void Save();
    }
}