using EducationPortal.BusinessLogic.Resources.ErrorMessages;
using EducationPortal.BusinessLogic.Validators.FluentValidation;
using EducationPortal.DataAccess.DomainModels.Materials;
using EducationPortal.DataAccess.Repositories;
using FluentResults;
using FluentValidation;

namespace EducationPortal.BusinessLogic.Services.MaterialServices;

public class VideoMaterialService
{
    private readonly IGenericRepository<VideoMaterial> _repository;

    public VideoMaterialService()
    {
        _repository = new GenericRepository<VideoMaterial>();
    }

    public VideoMaterialService(GenericRepository<VideoMaterial> repository)
    {
        _repository = repository;
    }

    public Result<VideoMaterial> GetVideoById(Guid id)
    {
        if (id == Guid.Empty)
        {
            return Result.Fail(ErrorMessages.GuidEmpty);
        }

        var result = Result.Try(() => _repository.FindFirst(x => x.Id == id,
            false,
            x => x.CreatedBy,
            x => x.UpdatedBy));

        if (result.IsSuccess && result.Value == null)
        {
            return Result.Fail(ErrorMessages.NotFound);
        }

        return result;
    }

    public Result<List<VideoMaterial>> GetAllVideos()
    {
        var result = Result.Try(() => _repository.FindAll(
            _ => true,
            true,
            x => x.CreatedBy,
            x => x.UpdatedBy).ToList());

        return result;
    }

    public Result Add(VideoMaterial material)
    {
        var result = ValidateVideo(material);
        if (result.IsFailed)
        {
            return result;
        }

        return Result.Try(() => _repository.Add(material));
    }

    public Result UpdateVideo(VideoMaterial material)
    {
        var result = ValidateVideo(material);
        if (result.IsFailed)
        {
            return result;
        }

        return Result.Try(() => _repository.Update(material));
    }

    public Result DeleteVideo(VideoMaterial material)
    {
        if (material == null)
        {
            return Result.Fail(ErrorMessages.ModelIsNull);
        }

        return Result.Try(() => _repository.Remove(material));
    }

    private Result ValidateVideo(VideoMaterial material)
    {
        if (material == null)
        {
            return Result.Fail(ErrorMessages.ModelIsNull);
        }

        var validator = new VideoMaterialValidator();
        try
        {
            validator.ValidateAndThrow(material);
        }
        catch (ValidationException e)
        {
            return Result.Fail(new Error(ErrorMessages.ValidationError).CausedBy(e));
        }

        return Result.Ok();
    }
}