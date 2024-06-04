using System.Collections;

namespace Base.Extensions;

public static class CollectionExtension
{
    public static bool IsNullOrEmpty(this IEnumerable @this)
    {
        return @this == null || @this.GetEnumerator().MoveNext() == false;
    }
}