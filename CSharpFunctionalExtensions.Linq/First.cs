namespace CSharpFunctionalExtensions.Linq;

public static class First
{
    public static Result<T> FirstOrError<T>(this IEnumerable<T> source, string errorMessage = "Sequence contains no elements") 
        => source.TryFirst().ToResult(errorMessage);

    public static Result<T, TE> FirstOrError<T, TE>(this IEnumerable<T> source, TE error) 
        => source.TryFirst().ToResult(error);

    public static Result<T, TE> FirstOrError<T, TE>(this IEnumerable<T> source, Func<TE> errorFactory) 
        => source.TryFirst().ToResult(errorFactory);

    public static Maybe<T> MaybeFirst<T>(this IEnumerable<T> source) 
        => source.TryFirst();
}