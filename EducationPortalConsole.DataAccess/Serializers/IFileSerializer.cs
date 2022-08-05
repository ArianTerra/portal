namespace EducationPortalConsole.DataAccess.Serializers;

public interface IFileSerializer<T>
{
    void Add(T entity);
    T? GetFirst(Func<T, bool> predicate);
    IEnumerable<T> FindAll(Func<T, bool> predicate);
    IEnumerable<T> GetAll();
    bool Delete(T entity);
    void Load();
    void Save();
}