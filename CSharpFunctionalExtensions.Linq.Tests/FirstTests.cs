namespace CSharpFunctionalExtensions.Linq.Tests;

public class FirstTests
{
    [Fact]
    public void FirstOrError_ShouldReturnError_WhenSequenceIsEmpty()
    {
        var list = new List<int>();

        var result = list.FirstOrError();

        Assert.False(result.IsSuccess);
        Assert.Equal("Sequence contains no elements", result.Error);
    }

    [Fact]
    public void FirstOrError_ShouldReturnFirstElement_WhenSequenceIsNotEmpty()
    {
        var list = new List<int> { 1, 2, 3 };

        var result = list.FirstOrError();

        Assert.True(result.IsSuccess);
        Assert.Equal(1, result.Value);
    }

    [Fact]
    public void FirstOrError_ShouldReturnCustomError_WhenSequenceIsEmpty()
    {
        var list = new List<int>();

        var result = list.FirstOrError("Custom error");

        Assert.False(result.IsSuccess);
        Assert.Equal("Custom error", result.Error);
    }

    [Fact]
    public void FirstOrError_ShouldReturnFirstElement_WhenSequenceIsNotEmpty_CustomError()
    {
        var list = new List<int> { 1, 2, 3 };

        var result = list.FirstOrError("Custom error");

        Assert.True(result.IsSuccess);
        Assert.Equal(1, result.Value);
    }

    [Fact]
    public void FirstOrError_ShouldReturnError_WhenSequenceIsEmpty_UsingFunc()
    {
        var list = new List<int>();

        var result = list.FirstOrError(() => "Error from Func");

        Assert.False(result.IsSuccess);
        Assert.Equal("Error from Func", result.Error);
    }

    [Fact]
    public void FirstOrMaybe_ShouldReturnNone_WhenSequenceIsEmpty()
    {
        var list = new List<int>();

        var result = list.MaybeFirst();

        Assert.True(result.HasNoValue);
    }

    [Fact]
    public void FirstOrMaybe_ShouldReturnFirstElement_WhenSequenceIsNotEmpty()
    {
        var list = new List<int> { 1, 2, 3 };

        var result = list.MaybeFirst();

        Assert.True(result.HasValue);
        Assert.Equal(1, result.Value);
    }
}