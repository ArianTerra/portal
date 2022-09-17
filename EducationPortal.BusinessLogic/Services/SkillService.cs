using EducationPortal.BusinessLogic.Resources.ErrorMessages;
using EducationPortal.BusinessLogic.Validators.FluentValidation;
using EducationPortal.DataAccess.DomainModels;
using EducationPortal.DataAccess.Repositories;
using FluentResults;
using FluentValidation;

namespace EducationPortal.BusinessLogic.Services;

public class SkillService
{
    private readonly IGenericRepository<Skill> _repository;

    public SkillService()
    {
        _repository = new GenericRepository<Skill>();
    }

    public SkillService(IGenericRepository<Skill> repository)
    {
        _repository = repository;
    }

    public Result<Skill> GetSkillById(Guid id)
    {
        if (id == Guid.Empty)
        {
            return Result.Fail(ErrorMessages.GuidEmpty);
        }

        var result = Result.Try(() =>
            _repository.FindFirst(
            x => x.Id == id,
            true,
            x => x.CreatedBy,
            x => x.UpdatedBy,
            x => x.CourseSkills,
            x => x.SkillProgresses));

        if (result.IsSuccess && result.Value == null)
        {
            return Result.Fail(ErrorMessages.SkillNotFound);
        }

        return result;
    }

    public Result<List<Skill>> GetAllSkills()
    {
        var repositoryResult = Result.Try(() =>
            _repository.FindAll(
            _ => true,
            false,
            x => x.CreatedBy,
            x => x.UpdatedBy).ToList());

        return repositoryResult;
    }

    public Result AddSkill(Skill skill)
    {
        var validationResult = ValidateSkill(skill);

        return validationResult.IsSuccess
            ? Result.Try(() => _repository.Add(skill))
            : validationResult;
    }

    public Result UpdateSkill(Skill skill)
    {
        var validationResult = ValidateSkill(skill);

        return validationResult.IsSuccess
            ? Result.Try(() => _repository.Update(skill))
            : validationResult;
    }

    public Result DeleteSkills(IEnumerable<Skill> skills)
    {
        if (skills == null || !skills.Any())
        {
            return Result.Fail(ErrorMessages.ModelIsNull);
        }

        return Result.Try(() => _repository.RemoveRange(skills));
    }

    private Result ValidateSkill(Skill skill)
    {
        if (skill == null)
        {
            return Result.Fail(ErrorMessages.ModelIsNull);
        }

        var validator = new SkillValidator();

        try
        {
            validator.ValidateAndThrow(skill);
        }
        catch (ValidationException e)
        {
            return Result.Fail(new Error(ErrorMessages.ValidationError).CausedBy(e));
        }

        return Result.Ok();
    }
}