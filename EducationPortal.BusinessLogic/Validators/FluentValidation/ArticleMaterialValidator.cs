using EducationPortal.DataAccess.DomainModels;
using EducationPortal.DataAccess.DomainModels.Materials;
using FluentValidation;
using Task1.DataAccess.Repository;

namespace EducationPortal.BusinessLogic.Validators.FluentValidation;

public class ArticleMaterialValidator : AbstractValidator<ArticleMaterial>
{
    private readonly IGenericRepository<Material> _repository;

    public ArticleMaterialValidator(IGenericRepository<Material> repository)
    {
        _repository = repository;

        RuleFor(x => x.Name)
            .NotEmpty();
            // .MustAsync(BeUniqueName)
            // .WithMessage("Field must be unique");
        RuleFor(x => x.Source).NotEmpty();
    }

    // private async Task<bool> BeUniqueName(string name, CancellationToken c)
    // {
    //     return await _repository.FindFirstAsync(x => x.Name == name) == null;
    // }
}