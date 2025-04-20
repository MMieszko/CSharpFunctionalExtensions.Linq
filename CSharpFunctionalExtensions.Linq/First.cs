namespace CSharpFunctionalExtensions.Linq;

/// <summary>
/// First extension methods for <see cref="IEnumerable{T}"/>.
/// </summary>
public static class First
{
    /// <summary>
    /// Attempts to retrieve the first element of the sequence.
    /// Returns a <see cref="Result{T}"/> containing the element if the sequence is not empty, or a failure result with the provided error message if the sequence is empty.
    /// </summary>
    /// <typeparam name="T">The type of elements in the source sequence.</typeparam>
    /// <param name="source">The sequence from which to retrieve the first element.</param>
    /// <param name="errorMessage">The error message to include in the failure result if the sequence is empty.</param>
    /// <returns>
    /// A <see cref="Result{T}"/> containing the first element of the sequence, or a failure result with the provided error message if the sequence is empty.
    /// </returns>
    public static Result<T> FirstOrError<T>(this IEnumerable<T> source, string errorMessage = "Sequence contains no elements") 
        => source.TryFirst().ToResult(errorMessage);

    /// <summary>
    /// Attempts to retrieve the first element of the sequence.
    /// Returns a <see cref="Result{T,TE}"/> containing the element if the sequence is not empty, or a failure result with the provided error if the sequence is empty.
    /// </summary>
    /// <typeparam name="T">The type of elements in the source sequence.</typeparam>
    /// <typeparam name="TE">The type of the error value.</typeparam>
    /// <param name="source">The sequence from which to retrieve the first element.</param>
    /// <param name="error">The error value to include in the failure result if the sequence is empty.</param>
    /// <returns>
    /// A <see cref="Result{T,TE}"/> containing the first element of the sequence, or a failure result with the provided error value if the sequence is empty.
    /// </returns>
    public static Result<T, TE> FirstOrError<T, TE>(this IEnumerable<T> source, TE error) 
        => source.TryFirst().ToResult(error);

    /// <summary>
    /// Attempts to retrieve the first element of the sequence.
    /// Returns a <see cref="Result{T,TE}"/> containing the element if the sequence is not empty, or a failure result with the error value produced by the provided factory function if the sequence is empty.
    /// </summary>
    /// <typeparam name="T">The type of elements in the source sequence.</typeparam>
    /// <typeparam name="TE">The type of the error value.</typeparam>
    /// <param name="source">The sequence from which to retrieve the first element.</param>
    /// <param name="errorFactory">A function that generates the error value to return if the sequence is empty.</param>
    /// <returns>
    /// A <see cref="Result{T,TE}"/> containing the first element of the sequence, or a failure result with the error value produced by <paramref name="errorFactory"/> if the sequence is empty.
    /// </returns>
    public static Result<T, TE> FirstOrError<T, TE>(this IEnumerable<T> source, Func<TE> errorFactory) 
        => source.TryFirst().ToResult(errorFactory);

    /// <summary>
    /// Attempts to retrieve the first element of the sequence.
    /// Returns a <see cref="Maybe{T}"/> containing the element if the sequence is not empty, or <see cref="Maybe{T}.None"/> if the sequence is empty.
    /// </summary>
    /// <typeparam name="T">The type of elements in the source sequence.</typeparam>
    /// <param name="source">The sequence from which to retrieve the first element.</param>
    /// <returns>
    /// A <see cref="Maybe{T}"/> containing the first element of the sequence, or <see cref="Maybe{T}.None"/> if the sequence is empty.
    /// </returns>
    public static Maybe<T> MaybeFirst<T>(this IEnumerable<T> source) 
        => source.TryFirst();
}