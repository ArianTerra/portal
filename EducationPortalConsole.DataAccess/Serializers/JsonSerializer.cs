using Newtonsoft.Json;

namespace EducationPortalConsole.DataAccess.Serializers;

public class JsonSerializer<T> : IFileSerializer<T>
{
    private List<T> _objects = new List<T>();
    private readonly string _filename;
    private readonly string _path = Environment.CurrentDirectory;
        
    public JsonSerializer()
    {
        //_filename = nameof() + ".json";
        throw new NotImplementedException();
    }

    public JsonSerializer(string filename)
    {
        _filename = filename + ".json";
    }

    public void Add(T entity)
    {
        _objects.Add(entity);
    }

    public T? GetFirst(Func<T, bool> predicate)
    {
        return _objects.Where(predicate).FirstOrDefault();
    }

    public IEnumerable<T> FindAll(Func<T, bool> predicate)
    {
        return _objects.Where(predicate);
    }

    public IEnumerable<T> GetAll()
    {
        return _objects.AsReadOnly(); //TODO should it be readonly???
    }

    public bool Delete(T entity)
    {
        return _objects.Remove(entity);
    }
        
    public void Save()
    {
        var json = JsonConvert.SerializeObject(_objects);
        File.WriteAllText(_path + "/" + _filename, json);
    }
        
    public void Load()
    {
        if (!File.Exists(_path + "/" + _filename))
        {
            //throw new FileNotFoundException($"File {_filename} not found");
            return;
        }

        var json = File.ReadAllText(_path + "/" + _filename);
        _objects = JsonConvert.DeserializeObject<List<T>>(json);
    }
}