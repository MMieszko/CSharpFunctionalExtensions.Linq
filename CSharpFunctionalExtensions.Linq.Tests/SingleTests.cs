namespace CSharpFunctionalExtensions.Linq.Tests;
public class SingleTests
{
    [Fact]
    public void SingleOrError_ShouldReturnError_WhenSequenceMultipleFound()
    {
        List<int> list = [1, 2, 2];

        var result = list.SingleOrError();

        Assert.False(result.IsSuccess);
        Assert.Equal("Sequence does not contain exactly one element", result.Error);
    }

    [Fact]
    public void SingleOrError_ShouldReturnNone_WhenSequenceMultipleFound()
    {
        List<int> list = [1, 2, 2];

        var result = list.MaybeSingle();

        Assert.Equal(Maybe<int>.None, result);
    }

    [Fact]
    public void SingleOrError_ShouldReturnError_WhenSequenceIsEmpty()
    {
        var list = new List<int>();

        var result = list.SingleOrError();

        Assert.False(result.IsSuccess);
        Assert.Equal("Sequence does not contain exactly one element", result.Error);
    }

    [Fact]
    public void SingleOrError_ShouldReturnError_WhenSequenceContainsMoreThanOneElement()
    {
        var list = new List<int> { 1, 2 };

        var result = list.SingleOrError();

        Assert.False(result.IsSuccess);
        Assert.Equal("Sequence does not contain exactly one element", result.Error);
    }

    [Fact]
    public void SingleOrError_ShouldReturnSingleElement_WhenSequenceContainsExactlyOneElement()
    {
        var list = new List<int> { 1 };

        var result = list.SingleOrError();

        Assert.True(result.IsSuccess);
        Assert.Equal(1, result.Value);
    }

    [Fact]
    public void SingleOrError_ShouldReturnCustomError_WhenSequenceIsEmpty()
    {
        var list = new List<int>();

        var result = list.SingleOrError("Custom error");

        Assert.False(result.IsSuccess);
        Assert.Equal("Custom error", result.Error);
    }

    [Fact]
    public void SingleOrError_ShouldReturnSingleElement_WhenSequenceContainsExactlyOneElement_CustomError()
    {
        var list = new List<int> { 1 };

        var result = list.SingleOrError("Custom error");

        Assert.True(result.IsSuccess);
        Assert.Equal(1, result.Value);
    }

    [Fact]
    public void SingleOrError_ShouldReturnError_WhenSequenceContainsMoreThanOneElement_CustomError()
    {
        var list = new List<int> { 1, 2 };

        var result = list.SingleOrError("Custom error");

        Assert.False(result.IsSuccess);
        Assert.Equal("Custom error", result.Error);
    }

    [Fact]
    public void SingleOrError_ShouldReturnError_WhenSequenceIsEmpty_UsingFunc()
    {
        var list = new List<int>();

        var result = list.SingleOrError(() => "Error from Func");

        Assert.False(result.IsSuccess);
        Assert.Equal("Error from Func", result.Error);
    }

    [Fact]
    public void SingleOrMaybe_ShouldReturnNone_WhenSequenceIsEmpty()
    {
        var list = new List<int>();

        var result = list.MaybeSingle(x => x > 1);

        Assert.True(result.HasNoValue);
    }

    [Fact]
    public void SingleOrMaybe_ShouldReturnSingleElement_WhenSequenceContainsExactlyOneElement()
    {
        var list = new List<int> { 1 };

        var result = list.MaybeSingle(x => x > 0);

        Assert.True(result.HasValue);
        Assert.Equal(1, result.Value);
    }

    [Fact]
    public void SingleOrMaybe_ShouldReturnNone_WhenSequenceContainsMoreThanOneElement()
    {
        var list = new List<int> { 1, 2 };

        var result = list.MaybeSingle(x => x > 0);

        Assert.True(result.HasNoValue);
    }

    [Fact]
    public void SingleOrMaybe_ShouldReturnNone_WhenPredicateDoesNotMatch()
    {
        var list = new List<int> { 1 };

        var result = list.MaybeSingle(x => x > 10);

        Assert.True(result.HasNoValue);
    }
}
