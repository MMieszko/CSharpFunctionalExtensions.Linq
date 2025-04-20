namespace CSharpFunctionalExtensions.Linq.Tests;

public class ToNonEmptyTests
{
    [Fact]
    public void ToNonEmptyList_WithEmptySequence_ReturnsFailure()
    {
        var source = Enumerable.Empty<int>();

        var result = source.ToNonEmptyList();

        Assert.True(result.IsFailure);
        Assert.Equal("Sequence contains no elements", result.Error);
    }

    [Fact]
    public void ToNonEmptyList_WithNonEmptySequence_ReturnsSuccessWithList()
    {
        var source = new[] { 1, 2, 3 };

        var result = source.ToNonEmptyList();

        Assert.True(result.IsSuccess);
        Assert.Equal(source, result.Value);
        Assert.IsType<List<int>>(result.Value);
    }

    [Fact]
    public void ToNonEmptyList_WithCustomErrorMessage_ReturnsFailureWithThatMessage()
    {
        var source = Enumerable.Empty<string>();

        var result = source.ToNonEmptyList("custom error");

        Assert.True(result.IsFailure);
        Assert.Equal("custom error", result.Error);
    }

    [Fact]
    public void ToNonEmptyList_WithErrorFactory_ReturnsFailureWithCustomError()
    {
        var source = Enumerable.Empty<string>();

        var result = source.ToNonEmptyList(() => "dynamic error");

        Assert.True(result.IsFailure);
        Assert.Equal("dynamic error", result.Error);
    }

    [Fact]
    public void ToNonEmptyList_WithErrorFactory_ReturnsSuccessOnValidInput()
    {
        var source = new[] { "a", "b" };

        var result = source.ToNonEmptyList(() => "dynamic error");

        Assert.True(result.IsSuccess);
        Assert.Equal(source, result.Value);
    }

    [Fact]
    public void MaybeToNonEmptyList_WithEmptySequence_ReturnsNone()
    {
        var source = Enumerable.Empty<int>();

        var maybe = source.MaybeToNonEmptyList();

        Assert.False(maybe.HasValue);
    }

    [Fact]
    public void MaybeToNonEmptyList_WithNonEmptySequence_ReturnsSome()
    {
        var source = new List<int> { 10, 20 };

        var maybe = source.MaybeToNonEmptyList();

        Assert.True(maybe.HasValue);
        Assert.Equal(source, maybe.Value);
    }

    [Fact]
    public void ToNonEmptyList_WithYieldSequence_ReturnsSuccess()
    {
        IEnumerable<string> GetItems()
        {
            yield return "one";
            yield return "two";
        }

        var result = GetItems().ToNonEmptyList();

        Assert.True(result.IsSuccess);
        Assert.Equal(new[] { "one", "two" }, result.Value);
    }
}
