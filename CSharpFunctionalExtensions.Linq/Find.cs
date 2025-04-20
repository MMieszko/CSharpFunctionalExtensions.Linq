namespace CSharpFunctionalExtensions.Linq;

public static class Find
{
    public static Result<T> FindOrError<T>(this IEnumerable<T> source, Func<T, bool> predicate, string errorMessage = "Element not found") 
        => source.TryFirst(predicate).ToResult(errorMessage);

    public static Result<T, TE> FindOrError<T, TE>(this IEnumerable<T> source, Func<T, bool> predicate, TE error) 
        => source.TryFirst(predicate).ToResult(error);

    public static Result<T, TE> FindOrError<T, TE>(this IEnumerable<T> source, Func<T, bool> predicate, Func<TE> errorFactory) 
        => source.TryFirst(predicate).ToResult(errorFactory);

    public static Maybe<T> MaybeFind<T>(this IEnumerable<T> source, Func<T, bool> predicate) 
        => source.TryFirst(predicate);
}