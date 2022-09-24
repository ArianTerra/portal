using EducationPortal.BusinessLogic.DTO;

namespace EducationPortal.BusinessLogic.Utils.Comparers;

public class BookAuthorDtoComparer : IEqualityComparer<BookAuthorDto>
{
    public bool Equals(BookAuthorDto x, BookAuthorDto y)
    {
        if (ReferenceEquals(x, y))
        {
            return true;
        }

        if (ReferenceEquals(x, null))
        {
            return false;
        }

        if (ReferenceEquals(y, null))
        {
            return false;
        }

        if (x.GetType() != y.GetType())
        {
            return false;
        }

        return x.Id == y.Id;
    }

    public int GetHashCode(BookAuthorDto obj)
    {
        return obj.Id.GetHashCode();
    }
}