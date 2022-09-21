using EducationPortal.BusinessLogic.DTO;
using FluentResults;

namespace EducationPortal.BusinessLogic.Services.Interfaces;

public interface IBookMaterialService
{
    Task<Result<BookMaterialDto>> GetBookByIdAsync(Guid id);

    Task<Result<IEnumerable<BookMaterialDto>>> GetBooksPageAsync(int page, int pageSize);

    Task<Result<int>> GetBooksCountAsync();

    Task<Result<Guid>> AddBookAsync(BookMaterialDto dto);

    Task<Result> UpdateBookAsync(BookMaterialDto dto);

    Task<Result> DeleteBookByIdAsync(Guid id);
}