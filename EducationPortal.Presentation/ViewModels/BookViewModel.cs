using EducationPortal.BusinessLogic.DTO;

namespace EducationPortal.Presentation.ViewModels;

public class BookViewModel
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public int Pages { get; set; }

    public string Format { get; set; }

    public IEnumerable<BookFormatDto> AvailableFormats { get; set; }

    public IEnumerable<BookAuthorDto> AvailableAuthors { get; set; }

    public IEnumerable<string> SelectedAuthors {get; set; }
}