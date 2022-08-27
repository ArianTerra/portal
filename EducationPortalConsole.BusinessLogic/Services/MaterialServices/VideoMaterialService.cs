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

    public VideoMaterial? GetVideoById(Guid id)
    {
        return _repository.FindFirst(x => x.Id == id,
            false,
            x => x.CreatedBy,
            x => x.UpdatedBy);
    }

    public IEnumerable<VideoMaterial> GetAllVideos()
    {
        return _repository.FindAll(
            _ => true,
            true,
            x => x.CreatedBy,
            x => x.UpdatedBy);
    }

    public void Add(VideoMaterial material)
    {
        _repository.Add(material);
    }

    public void UpdateVideo(VideoMaterial material)
    {
        _repository.Update(material);
    }

    public bool DeleteVideo(VideoMaterial material)
    {
        return _repository.Remove(material);
    }
}