namespace CSharpFunctionalExtensions.Linq;

/// <summary>
/// Last extension methods for <see cref="IEnumerable{T}"/>.
/// </summary>
public static class Last
{
    /// <summary>
    /// Attempts to retrieve the last element of the sequence.
    /// Returns a <see cref="Result{T}"/> containing the element if the sequence is not empty, or a failure result with the provided error message if the sequence is empty.
    /// </summary>
    /// <typeparam name="T">The type of elements in the source sequence.</typeparam>
    /// <param name="source">The sequence from which to retrieve the last element.</param>
    /// <param name="errorMessage">The error message to include in the failure result if the sequence is empty.</param>
    /// <returns>
    /// A <see cref="Result{T}"/> containing the last element of the sequence, or a failure result with the provided error message if the sequence is empty.
    /// </returns>
    public static Result<T> LastOrError<T>(this IEnumerable<T> source, string errorMessage = "Sequence contains no elements") => source.MaterializeList() switch
    {
        [] => Result.Failure<T>(errorMessage),
        var list => list[^1]
    };

    /// <summary>
    /// Attempts to retrieve the last element of the sequence.
    /// Returns a <see cref="Result{T,TE}"/> containing the element if the sequence is not empty, or a failure result with the provided error if the sequence is empty.
    /// </summary>
    /// <typeparam name="T">The type of elements in the source sequence.</typeparam>
    /// <typeparam name="TE">The type of the error value.</typeparam>
    /// <param name="source">The sequence from which to retrieve the last element.</param>
    /// <param name="error">The error value to include in the failure result if the sequence is empty.</param>
    /// <returns>
    /// A <see cref="Result{T,TE}"/> containing the last element of the sequence, or a failure result with the provided error value if the sequence is empty.
    /// </returns>
    public static Result<T, TE> LastOrError<T, TE>(this IEnumerable<T> source, TE error) => source.MaterializeList() switch
    {
        [] => Result.Failure<T, TE>(error),
        var list => list[^1]
    };

    /// <summary>
    /// Attempts to retrieve the last element of the sequence.
    /// Returns a <see cref="Result{T,TE}"/> containing the element if the sequence is not empty, or a failure result with the error value produced by the provided factory function if the sequence is empty.
    /// </summary>
    /// <typeparam name="T">The type of elements in the source sequence.</typeparam>
    /// <typeparam name="TE">The type of the error value.</typeparam>
    /// <param name="source">The sequence from which to retrieve the last element.</param>
    /// <param name="errorFactory">A function that generates the error value to return if the sequence is empty.</param>
    /// <returns>
    /// A <see cref="Result{T,TE}"/> containing the last element of the sequence, or a failure result with the error value produced by <paramref name="errorFactory"/> if the sequence is empty.
    /// </returns>
    public static Result<T, TE> LastOrError<T, TE>(this IEnumerable<T> source, Func<TE> errorFactory) => source.MaterializeList() switch
    {
        [] => Result.Failure<T, TE>(errorFactory()),
        var list => list[^1]
    };

    /// <summary>
    /// Attempts to retrieve the last element of the sequence.
    /// Returns a <see cref="Maybe{T}"/> containing the element if the sequence is not empty, or <see cref="Maybe{T}.None"/> if the sequence is empty.
    /// </summary>
    /// <typeparam name="T">The type of elements in the source sequence.</typeparam>
    /// <param name="source">The sequence from which to retrieve the last element.</param>
    /// <returns>
    /// A <see cref="Maybe{T}"/> containing the last element of the sequence, or <see cref="Maybe{T}.None"/> if the sequence is empty.
    /// </returns>
    public static Maybe<T> MaybeLast<T>(this IEnumerable<T> source) => source.MaterializeList() switch
    {
        [] => Maybe.None,
        var list => list[^1]
    };

    /// <summary>
    /// Attempts to retrieve the last element of the sequence that matches a given predicate.
    /// Returns a <see cref="Maybe{T}"/> containing the element if the sequence is not empty and the predicate is satisfied, or <see cref="Maybe{T}.None"/> if no element satisfies the predicate or if the sequence is empty.
    /// </summary>
    /// <typeparam name="T">The type of elements in the source sequence.</typeparam>
    /// <param name="source">The sequence from which to retrieve the last element matching the predicate.</param>
    /// <param name="predicate">A function to test each element for a match.</param>
    /// <returns>
    /// A <see cref="Maybe{T}"/> containing the last element that matches the predicate, or <see cref="Maybe{T}.None"/> if no match is found or the sequence is empty.
    /// </returns>
    public static Maybe<T> MaybeLast<T>(this IEnumerable<T> source, Func<T, bool> predicate) => source.Where(predicate).MaterializeList() switch
    {
        [] => Maybe.None,
        var list => list[^1]
    };
}