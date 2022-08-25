using EducationPortalConsole.Core.Entities.Materials;
using EducationPortalConsole.DataAccess.Repositories;

namespace EducationPortalConsole.BusinessLogic.Services.MaterialServices;

public class VideoMaterialService
{
    private readonly IGenericRepository<VideoMaterial> _repository;

    public VideoMaterialService()
    {
        _repository = new GenericRepository<VideoMaterial>();
    }

    public VideoMaterial? GetById(Guid id)
    {
        return _repository.FindFirst(x => x.Id == id,
            x => x.CreatedBy,
            x => x.UpdatedBy);
    }

    public IEnumerable<VideoMaterial> GetAll()
    {
        return _repository.FindAll(
            _ => true,
            x => x.CreatedBy,
            x => x.UpdatedBy);
    }

    public void Add(VideoMaterial material)
    {
        _repository.Add(material);
    }

    public void Update(VideoMaterial material)
    {
        _repository.Update(material);
    }

    public bool Delete(VideoMaterial material)
    {
        return _repository.Delete(material);
    }
}