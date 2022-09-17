using EducationPortalConsole.Core.Entities.JoinEntities;

namespace EducationPortalConsole.BusinessLogic.Utils.Comparers;

public class BookAuthorBookMaterialComparer : IEqualityComparer<BookAuthorBookMaterial>
{
    public bool Equals(BookAuthorBookMaterial? x, BookAuthorBookMaterial? y)
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

        return x.BookAuthorId.Equals(y.BookAuthorId) && x.BookMaterialId.Equals(y.BookMaterialId);
    }

    public int GetHashCode(BookAuthorBookMaterial obj)
    {
        return HashCode.Combine(obj.BookAuthorId, obj.BookMaterialId);
    }
}