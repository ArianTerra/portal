using EducationPortalConsole.BusinessLogic.Resources.ErrorMessages;
using EducationPortalConsole.BusinessLogic.Validators.FluentValidation;
using EducationPortalConsole.Core.Entities;
using EducationPortalConsole.DataAccess.Repositories;
using FluentResults;
using FluentValidation;

namespace EducationPortalConsole.BusinessLogic.Services;

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
            return Result.Fail(ErrorMessages.SkillGuidEmpty);
        }

        var repositoryResult = Result.Try(() =>
            _repository.FindFirst(
            x => x.Id == id,
            true,
            x => x.CreatedBy,
            x => x.UpdatedBy,
            x => x.CourseSkills,
            x => x.SkillProgresses));

        if (repositoryResult.IsFailed)
        {
            return repositoryResult;
        }

        var skill = repositoryResult.Value;

        if (skill == null)
        {
            return Result.Fail(ErrorMessages.UserIsNull);
        }

        return Result.Ok(skill);
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
            return Result.Fail(ErrorMessages.SkillIsNull);
        }

        return Result.Try(() => _repository.RemoveRange(skills));
    }

    private Result ValidateSkill(Skill skill)
    {
        if (skill == null)
        {
            return Result.Fail(ErrorMessages.SkillIsNull);
        }

        var validator = new SkillValidator();

        try
        {
            validator.ValidateAndThrow(skill);
        }
        catch (ValidationException e)
        {
            return Result.Fail(new Error(ErrorMessages.SkillValidationError).CausedBy(e));
        }

        return Result.Ok();
    }
}