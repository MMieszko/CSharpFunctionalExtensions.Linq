namespace CSharpFunctionalExtensions.Linq;

internal static class Internal
{
    public static List<T> MaterializeList<T>(this IEnumerable<T> source) => source switch
    {
        List<T> list => list,
        ICollection<T> col => new List<T>(col),
        _ => source.ToList()
    };
}