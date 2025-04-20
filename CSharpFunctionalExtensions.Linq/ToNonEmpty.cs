namespace CSharpFunctionalExtensions.Linq;

public static class ToNonEmpty
{
    public static Result<List<T>> ToNonEmptyList<T>(this IEnumerable<T> source, string errorMessage = "Sequence contains no elements") => source.MaterializeList() switch
    {
        { Count: 0 } => Result.Failure<List<T>>(errorMessage),
        var result => result
    };

    public static Result<List<T>, TE> ToNonEmptyList<T, TE>(this IEnumerable<T> source, Func<TE> errorFactory) => source.MaterializeList() switch
    {
        { Count: 0 } => Result.Failure<List<T>, TE>(errorFactory()),
        var result => result
    };

    public static Maybe<List<T>> MaybeToNonEmptyList<T>(this IEnumerable<T> source) => source.MaterializeList() switch
    {
        { Count: 0 } => Maybe.None,
        var result => result
    };
}