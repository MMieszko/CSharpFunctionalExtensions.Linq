namespace CSharpFunctionalExtensions.Linq;

public static class Last
{
    public static Result<T> LastOrError<T>(this IEnumerable<T> source, string errorMessage = "Sequence contains no elements") => source.MaterializeList() switch
    {
        [] => Result.Failure<T>(errorMessage),
        var list => list[^1]
    };

    public static Result<T, TE> LastOrError<T, TE>(this IEnumerable<T> source, TE error) => source.MaterializeList() switch
    {
        [] => Result.Failure<T, TE>(error),
        var list => list[^1]
    };

    public static Result<T, TE> LastOrError<T, TE>(this IEnumerable<T> source, Func<TE> errorFactory) => source.MaterializeList() switch
    {
        [] => Result.Failure<T, TE>(errorFactory()),
        var list => list[^1]
    };

    public static Maybe<T> MaybeLast<T>(this IEnumerable<T> source) => source.MaterializeList() switch
    {
        [] => Maybe.None,
        var list => list[^1]
    };

    public static Maybe<T> MaybeLast<T>(this IEnumerable<T> source, Func<T, bool> predicate) => source.Where(predicate).MaterializeList() switch
    {
        [] => Maybe.None,
        var list => list[^1]
    };
}