namespace CSharpFunctionalExtensions.Linq;

/// <summary>
/// Find extension methods for <see cref="IEnumerable{T}"/>.
/// </summary>
public static class Find
{/// <summary>
    /// Attempts to find the first element in the sequence that satisfies the provided predicate.
    /// Returns a <see cref="Result{T}"/> containing the element if found, or a failure result with the provided error message if not.
    /// </summary>
    /// <typeparam name="T">The type of elements in the source sequence.</typeparam>
    /// <param name="source">The sequence to search.</param>
    /// <param name="predicate">The predicate to apply to each element in the sequence.</param>
    /// <param name="errorMessage">The error message to include in the failure result if the element is not found.</param>
    /// <returns>
    /// A <see cref="Result{T}"/> containing the first element that satisfies the predicate, or a failure result with the provided error message.
    /// </returns>
    
    public static Result<T> FindOrError<T>(this IEnumerable<T> source, Func<T, bool> predicate, string errorMessage = "Element not found")
        => source.TryFirst(predicate).ToResult(errorMessage);

    /// <summary>
    /// Attempts to find the first element in the sequence that satisfies the provided predicate.
    /// Returns a <see cref="Result{T,TE}"/> containing the element if found, or a failure result with the provided error if not.
    /// </summary>
    /// <typeparam name="T">The type of elements in the source sequence.</typeparam>
    /// <typeparam name="TE">The type of the error value.</typeparam>
    /// <param name="source">The sequence to search.</param>
    /// <param name="predicate">The predicate to apply to each element in the sequence.</param>
    /// <param name="error">The error value to include in the failure result if the element is not found.</param>
    /// <returns>
    /// A <see cref="Result{T,TE}"/> containing the first element that satisfies the predicate, or a failure result with the provided error value.
    /// </returns>
    public static Result<T, TE> FindOrError<T, TE>(this IEnumerable<T> source, Func<T, bool> predicate, TE error)
        => source.TryFirst(predicate).ToResult(error);

    /// <summary>
    /// Attempts to find the first element in the sequence that satisfies the provided predicate.
    /// Returns a <see cref="Result{T,TE}"/> containing the element if found, or a failure result with the error value produced by the provided factory function if not.
    /// </summary>
    /// <typeparam name="T">The type of elements in the source sequence.</typeparam>
    /// <typeparam name="TE">The type of the error value.</typeparam>
    /// <param name="source">The sequence to search.</param>
    /// <param name="predicate">The predicate to apply to each element in the sequence.</param>
    /// <param name="errorFactory">A function that generates the error value to return if the element is not found.</param>
    /// <returns>
    /// A <see cref="Result{T,TE}"/> containing the first element that satisfies the predicate, or a failure result with the error value produced by <paramref name="errorFactory"/>.
    /// </returns>
    public static Result<T, TE> FindOrError<T, TE>(this IEnumerable<T> source, Func<T, bool> predicate, Func<TE> errorFactory)
        => source.TryFirst(predicate).ToResult(errorFactory);

    /// <summary>
    /// Attempts to find the first element in the sequence that satisfies the provided predicate.
    /// Returns a <see cref="Maybe{T}"/> containing the element if found, or <see cref="Maybe{T}.None"/> if not.
    /// </summary>
    /// <typeparam name="T">The type of elements in the source sequence.</typeparam>
    /// <param name="source">The sequence to search.</param>
    /// <param name="predicate">The predicate to apply to each element in the sequence.</param>
    /// <returns>
    /// A <see cref="Maybe{T}"/> containing the first element that satisfies the predicate, or <see cref="Maybe{T}.None"/> if no element is found.
    /// </returns>
    public static Maybe<T> MaybeFind<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        => source.TryFirst(predicate);
}