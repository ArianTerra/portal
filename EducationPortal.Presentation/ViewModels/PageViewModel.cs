namespace EducationPortal.Presentation.ViewModels;

public class PageViewModel<T>
{
    public IEnumerable<T> Items { get; set; }

    public int Page { get; set; }

    public int PageSize { get; set; }

    public int PageCount { get; set; }
}