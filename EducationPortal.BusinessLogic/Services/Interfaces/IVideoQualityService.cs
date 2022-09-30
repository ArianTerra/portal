using EducationPortal.BusinessLogic.DTO;
using FluentResults;

namespace EducationPortal.BusinessLogic.Services.Interfaces;

public interface IVideoQualityService
{
    Task<Result<VideoQualityDto>> GetVideoQualityByIdAsync(Guid id);

    Task<Result<VideoQualityDto>> GetVideoQualityByNameAsync(string name);

    Task<Result<IEnumerable<VideoQualityDto>>> GetVideoQualityPageAsync(int page, int pageSize);

    Task<Result<IEnumerable<VideoQualityDto>>> GetAllVideoQualitiesAsync();

    Task<Result<int>> GetVideoQualitiesCountAsync();

    Task<Result<Guid>> AddVideoQualityAsync(VideoQualityDto dto);

    Task<Result> UpdateVideoQualityAsync(VideoQualityDto dto);

    Task<Result> DeleteVideoQualityByIdAsync(Guid id);
}