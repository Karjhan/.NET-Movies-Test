namespace Core.Extensions;

public static class EnumerableExtensions
{
    public static bool ContainsMultiple<T>(this IEnumerable<T> container, IEnumerable<T> valuesToCheck)
    {
        foreach (var value in valuesToCheck)
        {
            if (!container.Contains(value))
            {
                return false;
            }
        }
        return true;
    }
}