using EducationPortal.BusinessLogic.DTO;
using FluentResults;

namespace EducationPortal.BusinessLogic.Services.Interfaces;

public interface IBookFormatService
{
    Task<Result<BookFormatDto>> GetBookFormatByIdAsync(Guid id);

    Task<Result<BookFormatDto>> GetBookFormatByNameAsync(string name);

    Task<Result<IEnumerable<BookFormatDto>>> GetBookFormatPageAsync(int page, int pageSize);

    Task<Result<IEnumerable<BookFormatDto>>> GetAllBookFormatesAsync();

    Task<Result<int>> GetBookFormatsCountAsync();

    Task<Result<Guid>> AddBookFormatAsync(BookFormatDto dto);

    Task<Result> UpdateBookFormatAsync(BookFormatDto dto);

    Task<Result> DeleteBookFormatByIdAsync(Guid id);
}