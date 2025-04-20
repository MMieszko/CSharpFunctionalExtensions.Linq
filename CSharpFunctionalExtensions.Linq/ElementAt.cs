namespace CSharpFunctionalExtensions.Linq;

public static class ElementAt
{
    public static Result<T> ElementAtOrError<T>(this IEnumerable<T> source, int index, string errorMessage = "Element not found")
        => Implementation(source, index, () => Result.Failure<T>(errorMessage), Result.Success);

    public static Result<T, TE> ElementAtOrError<T, TE>(this IEnumerable<T> source, int index, TE error)
        => Implementation(source, index, () => Result.Failure<T, TE>(error), Result.Success<T, TE>);

    public static Result<T, TE> ElementAtOrError<T, TE>(this IEnumerable<T> source, int index, Func<TE> errorFactory)
        => Implementation(source, index, () => Result.Failure<T, TE>(errorFactory()), Result.Success<T, TE>);

    public static Maybe<T> MaybeElementAt<T>(this IEnumerable<T> source, int index)
        => Implementation(source, index, () => Maybe<T>.None, Maybe.From);

    private static TR Implementation<TR, T>(this IEnumerable<T> source, int index, Func<TR> onFail, Func<T, TR> onSuccess)
    {
        if (index < 0)
            return onFail();

        using var enumerator = source.GetEnumerator();

        for (var i = 0; i <= index; i++)
        {
            if (!enumerator.MoveNext())
                return onFail();
        }

        return onSuccess(enumerator.Current);
    }
}