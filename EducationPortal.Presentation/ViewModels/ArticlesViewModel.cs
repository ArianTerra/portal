using EducationPortal.BusinessLogic.DTO;

namespace EducationPortal.Presentation.ViewModels;

public class ArticlesViewModel
{
    public IEnumerable<ArticleMaterialDto> Items { get; set; }

    public int Page { get; set; }

    public int PageSize { get; set; }

    public int PageCount { get; set; }
}