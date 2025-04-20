namespace CSharpFunctionalExtensions.Linq;

public static class Single
{
    public static Result<T> SingleOrError<T>(this IEnumerable<T> source, string errorMessage = "Sequence does not contain exactly one element") 
        => TrySingle(source).ToResult(errorMessage);

    public static Result<T, TE> SingleOrError<T, TE>(this IEnumerable<T> source, Func<TE> errorFactory) 
        => TrySingle(source).ToResult(errorFactory());

    public static Result<T, TE> SingleOrError<T, TE>(this IEnumerable<T> source, TE error) 
        => TrySingle(source).ToResult(error);

    public static Result<T, TE> SingleOrError<T, TE>(this IEnumerable<T> source, Func<T, bool> predicate, Func<TE> errorFactory)
        => TrySingle(source, predicate).ToResult(errorFactory);

    public static Maybe<T> MaybeSingle<T>(this IEnumerable<T> source)
        => TrySingle(source);

    public static Maybe<T> MaybeSingle<T>(this IEnumerable<T> source, Func<T, bool> predicate) 
        => TrySingle(source, predicate);

    private static Maybe<T> TrySingle<T>(IEnumerable<T> source)
    {
        using var enumerator = source.GetEnumerator();

        if (!enumerator.MoveNext()) return Maybe<T>.None;

        var single = enumerator.Current;

        return !enumerator.MoveNext()
            ? Maybe.From(single) 
            : Maybe<T>.None;
    }

    private static Maybe<T> TrySingle<T>(IEnumerable<T> source, Func<T, bool> predicate)
    {
        using var enumerator = source.Where(predicate).GetEnumerator();

        if (!enumerator.MoveNext()) return Maybe<T>.None;

        var single = enumerator.Current;

        return !enumerator.MoveNext()
            ? Maybe.From(single)
            : Maybe<T>.None;
    }
}