namespace CSharpFunctionalExtensions.Linq.Tests;

public class LastTests
{
    [Fact]
    public void LastOrError_ShouldReturnError_WhenSequenceIsEmpty()
    {
        var list = new List<int>();

        var result = list.LastOrError();

        Assert.False(result.IsSuccess);
        Assert.Equal("Sequence contains no elements", result.Error);
    }

    [Fact]
    public void LastOrError_ShouldReturnLastElement_WhenSequenceIsNotEmpty()
    {
        var list = new List<int> { 1, 2, 3 };

        var result = list.LastOrError();

        Assert.True(result.IsSuccess);
        Assert.Equal(3, result.Value);
    }

    [Fact]
    public void LastOrError_ShouldReturnCustomError_WhenSequenceIsEmpty()
    {
        var list = new List<int>();

        var result = list.LastOrError("Custom error");

        Assert.False(result.IsSuccess);
        Assert.Equal("Custom error", result.Error);
    }

    [Fact]
    public void LastOrError_ShouldReturnLastElement_WhenSequenceIsNotEmpty_CustomError()
    {
        var list = new List<int> { 1, 2, 3 };

        var result = list.LastOrError("Custom error");

        Assert.True(result.IsSuccess);
        Assert.Equal(3, result.Value);
    }

    [Fact]
    public void LastOrError_ShouldReturnError_WhenSequenceIsEmpty_UsingFunc()
    {
        var list = new List<int>();

        var result = list.LastOrError(() => "Error from Func");

        Assert.False(result.IsSuccess);
        Assert.Equal("Error from Func", result.Error);
    }

    [Fact]
    public void LastOrMaybe_ShouldReturnNone_WhenSequenceIsEmpty()
    {
        var list = new List<int>();

        var result = list.MaybeLast();

        Assert.True(result.HasNoValue);
    }

    [Fact]
    public void LastOrMaybe_ShouldReturnLastElement_WhenSequenceIsNotEmpty()
    {
        var list = new List<int> { 1, 2, 3 };

        var result = list.MaybeLast();

        Assert.True(result.HasValue);
        Assert.Equal(3, result.Value);
    }

    [Fact]
    public void LastOrMaybe_ShouldReturnNone_WhenPredicateDoesNotMatch()
    {
        var list = new List<int> { 1, 2, 3 };

        var result = list.MaybeLast(x => x > 3);

        Assert.True(result.HasNoValue);
    }

    [Fact]
    public void LastOrMaybe_ShouldReturnLastElement_WhenPredicateMatches()
    {
        var list = new List<int> { 1, 2, 3 };

        var result = list.MaybeLast(x => x > 1);

        Assert.True(result.HasValue);
        Assert.Equal(3, result.Value);
    }
}