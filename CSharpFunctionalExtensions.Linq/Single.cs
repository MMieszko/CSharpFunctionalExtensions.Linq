namespace CSharpFunctionalExtensions.Linq;

/// <summary>
/// Single extension methods for <see cref="IEnumerable{T}"/>.
/// </summary>
public static class Single
{
    /// <summary>
    /// Attempts to retrieve the single element from the sequence.
    /// Returns a <see cref="Result{T}"/> containing the element if the sequence contains exactly one element, or a failure result with the provided error message if the sequence is empty or contains more than one element.
    /// </summary>
    /// <typeparam name="T">The type of elements in the source sequence.</typeparam>
    /// <param name="source">The sequence from which to retrieve the single element.</param>
    /// <param name="errorMessage">The error message to include in the failure result if the sequence does not contain exactly one element.</param>
    /// <returns>
    /// A <see cref="Result{T}"/> containing the single element of the sequence, or a failure result with the provided error message if the sequence does not contain exactly one element.
    /// </returns>
    public static Result<T> SingleOrError<T>(this IEnumerable<T> source, string errorMessage = "Sequence does not contain exactly one element")
        => TrySingle(source).ToResult(errorMessage);

    /// <summary>
    /// Attempts to retrieve the single element from the sequence.
    /// Returns a <see cref="Result{T,TE}"/> containing the element if the sequence contains exactly one element, or a failure result with the error produced by the provided factory if the sequence is empty or contains more than one element.
    /// </summary>
    /// <typeparam name="T">The type of elements in the source sequence.</typeparam>
    /// <typeparam name="TE">The type of the error value.</typeparam>
    /// <param name="source">The sequence from which to retrieve the single element.</param>
    /// <param name="errorFactory">A function that generates the error value to return if the sequence does not contain exactly one element.</param>
    /// <returns>
    /// A <see cref="Result{T,TE}"/> containing the single element of the sequence, or a failure result with the error value produced by <paramref name="errorFactory"/> if the sequence does not contain exactly one element.
    /// </returns>
    public static Result<T, TE> SingleOrError<T, TE>(this IEnumerable<T> source, Func<TE> errorFactory)
        => TrySingle(source).ToResult(errorFactory());

    /// <summary>
    /// Attempts to retrieve the single element from the sequence.
    /// Returns a <see cref="Result{T,TE}"/> containing the element if the sequence contains exactly one element, or a failure result with the provided error value if the sequence is empty or contains more than one element.
    /// </summary>
    /// <typeparam name="T">The type of elements in the source sequence.</typeparam>
    /// <typeparam name="TE">The type of the error value.</typeparam>
    /// <param name="source">The sequence from which to retrieve the single element.</param>
    /// <param name="error">The error value to return if the sequence does not contain exactly one element.</param>
    /// <returns>
    /// A <see cref="Result{T,TE}"/> containing the single element of the sequence, or a failure result with the provided error value if the sequence does not contain exactly one element.
    /// </returns>
    public static Result<T, TE> SingleOrError<T, TE>(this IEnumerable<T> source, TE error)
        => TrySingle(source).ToResult(error);

    /// <summary>
    /// Attempts to retrieve the single element from the sequence that satisfies the specified predicate.
    /// Returns a <see cref="Result{T,TE}"/> containing the element if the sequence contains exactly one element that matches the predicate, or a failure result with the error produced by the provided factory if the sequence is empty or contains more than one matching element.
    /// </summary>
    /// <typeparam name="T">The type of elements in the source sequence.</typeparam>
    /// <typeparam name="TE">The type of the error value.</typeparam>
    /// <param name="source">The sequence from which to retrieve the single element matching the predicate.</param>
    /// <param name="predicate">A function to test each element for a match.</param>
    /// <param name="errorFactory">A function that generates the error value to return if the sequence does not contain exactly one element matching the predicate.</param>
    /// <returns>
    /// A <see cref="Result{T,TE}"/> containing the single element that matches the predicate, or a failure result with the error value produced by <paramref name="errorFactory"/> if the sequence does not contain exactly one matching element.
    /// </returns>
    public static Result<T, TE> SingleOrError<T, TE>(this IEnumerable<T> source, Func<T, bool> predicate, Func<TE> errorFactory)
        => TrySingle(source, predicate).ToResult(errorFactory);

    /// <summary>
    /// Attempts to retrieve the single element from the sequence.
    /// Returns a <see cref="Maybe{T}"/> containing the element if the sequence contains exactly one element, or <see cref="Maybe{T}.None"/> if the sequence is empty or contains more than one element.
    /// </summary>
    /// <typeparam name="T">The type of elements in the source sequence.</typeparam>
    /// <param name="source">The sequence from which to retrieve the single element.</param>
    /// <returns>
    /// A <see cref="Maybe{T}"/> containing the single element of the sequence, or <see cref="Maybe{T}.None"/> if the sequence does not contain exactly one element.
    /// </returns>
    public static Maybe<T> MaybeSingle<T>(this IEnumerable<T> source)
        => TrySingle(source);

    /// <summary>
    /// Attempts to retrieve the single element from the sequence that satisfies the specified predicate.
    /// Returns a <see cref="Maybe{T}"/> containing the element if the sequence contains exactly one element that matches the predicate, or <see cref="Maybe{T}.None"/> if no element matches the predicate or if the sequence contains more than one matching element.
    /// </summary>
    /// <typeparam name="T">The type of elements in the source sequence.</typeparam>
    /// <param name="source">The sequence from which to retrieve the single element matching the predicate.</param>
    /// <param name="predicate">A function to test each element for a match.</param>
    /// <returns>
    /// A <see cref="Maybe{T}"/> containing the single element that matches the predicate, or <see cref="Maybe{T}.None"/> if no element matches or if the sequence contains more than one matching element.
    /// </returns>
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