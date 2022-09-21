using EducationPortal.BusinessLogic.DTO;
using FluentResults;

namespace EducationPortal.BusinessLogic.Services.Interfaces;

public interface IBookAuthorService
{
    Task<Result<BookAuthorDto>> GetBookAuthorById(Guid id);

    Task<Result<IEnumerable<BookAuthorDto>>> GetBookAuthorsPageAsync(int page, int pageSize);

    Task<Result<int>> GetBookAuthorCountAsync();

    Task<Result<Guid>> AddBookAuthorAsync(BookAuthorDto dto);

    Task<Result> UpdateBookAuthorAsync(BookAuthorDto dto);

    Task<Result> DeleteBookAuthorByIdAsync(Guid id);
}