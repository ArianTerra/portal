using EducationPortalConsole.Core.Entities;
using EducationPortalConsole.DataAccess.Repositories;

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

    public Skill? GetSkillById(Guid id)
    {
        return _repository.FindFirst(x => x.Id == id,
            true,
            x => x.CreatedBy,
            x => x.UpdatedBy,
            x => x.CourseSkills,
            x => x.SkillProgresses);
    }

    public IEnumerable<Skill> GetAllSkills()
    {
        return _repository.FindAll(
            _ => true,
            true,
            x => x.CreatedBy,
            x => x.UpdatedBy,
            x => x.CourseSkills,
            x => x.SkillProgresses);
    }

    public void AddSkill(Skill skill)
    {
        if (skill == null)
        {
            //TODO validation
        }

        _repository.Add(skill);
    }

    public void UpdateSkill(Skill skill)
    {
        _repository.Update(skill);
    }

    public void DeleteSkills(IEnumerable<Skill> skills)
    {
        _repository.RemoveRange(skills);
    }
}