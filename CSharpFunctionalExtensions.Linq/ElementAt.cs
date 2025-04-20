namespace CSharpFunctionalExtensions.Linq;

/// <summary>
/// ElementAt extension methods for <see cref="IEnumerable{T}"/>.
/// </summary>
public static class ElementAt
{
    /// <summary>
    /// Returns a <see cref="Result{T}"/> containing the element at the specified index in the sequence,
    /// or a failure result if the index is out of range.
    /// </summary>
    /// <typeparam name="T">The type of elements in the source sequence.</typeparam>
    /// <param name="source">The sequence to retrieve the element from.</param>
    /// <param name="index">The zero-based index of the element to retrieve.</param>
    /// <param name="errorMessage">The error message to include in the failure result if the index is invalid.</param>
    /// <returns>
    /// A <see cref="Result{T}"/> containing the element if the index is valid; otherwise, a failure result with the specified error message.
    /// </returns>
    public static Result<T> ElementAtOrError<T>(this IEnumerable<T> source, int index, string errorMessage = "Element not found")
        => Implementation(source, index, () => Result.Failure<T>(errorMessage), Result.Success);

    /// <summary>
    /// Returns a <see cref="Result{T,TE}"/> containing the element at the specified index in the sequence,
    /// or a failure result with the provided error value if the index is out of range.
    /// </summary>
    /// <typeparam name="T">The type of elements in the source sequence.</typeparam>
    /// <typeparam name="TE">The type of the error value.</typeparam>
    /// <param name="source">The sequence to retrieve the element from.</param>
    /// <param name="index">The zero-based index of the element to retrieve.</param>
    /// <param name="error">The error value to return if the index is invalid.</param>
    /// <returns>
    /// A <see cref="Result{T,TE}"/> containing the element if the index is valid; otherwise, a failure result with the specified error value.
    /// </returns>
    public static Result<T, TE> ElementAtOrError<T, TE>(this IEnumerable<T> source, int index, TE error)
        => Implementation(source, index, () => Result.Failure<T, TE>(error), Result.Success<T, TE>);

    /// <summary>
    /// Returns a <see cref="Result{T,TE}"/> containing the element at the specified index in the sequence,
    /// or a failure result using a factory to produce the error value if the index is out of range.
    /// </summary>
    /// <typeparam name="T">The type of elements in the source sequence.</typeparam>
    /// <typeparam name="TE">The type of the error value.</typeparam>
    /// <param name="source">The sequence to retrieve the element from.</param>
    /// <param name="index">The zero-based index of the element to retrieve.</param>
    /// <param name="errorFactory">A function that produces the error value to return if the index is invalid.</param>
    /// <returns>
    /// A <see cref="Result{T,TE}"/> containing the element if the index is valid; otherwise, a failure result with the error produced by <paramref name="errorFactory"/>.
    /// </returns>
    public static Result<T, TE> ElementAtOrError<T, TE>(this IEnumerable<T> source, int index, Func<TE> errorFactory)
        => Implementation(source, index, () => Result.Failure<T, TE>(errorFactory()), Result.Success<T, TE>);

    /// <summary>
    /// Returns a <see cref="Maybe{T}"/> containing the element at the specified index in the sequence,
    /// or <see cref="Maybe{T}.None"/> if the index is out of range.
    /// </summary>
    /// <typeparam name="T">The type of elements in the source sequence.</typeparam>
    /// <param name="source">The sequence to retrieve the element from.</param>
    /// <param name="index">The zero-based index of the element to retrieve.</param>
    /// <returns>
    /// A <see cref="Maybe{T}"/> containing the element if the index is valid; otherwise, <see cref="Maybe{T}.None"/>.
    /// </returns>
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