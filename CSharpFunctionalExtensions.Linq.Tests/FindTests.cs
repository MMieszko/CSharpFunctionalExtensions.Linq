namespace CSharpFunctionalExtensions.Linq.Tests;

public class FindTests
{
    [Fact]
    public void FindOrError_ShouldReturnError_WhenElementNotFound()
    {
        var list = new List<int> { 1, 2, 3 };

        var result = list.FindOrError(x => x == 4);

        Assert.False(result.IsSuccess);
        Assert.Equal("Element not found", result.Error);
    }

    [Fact]
    public void FindOrError_ShouldReturnElement_WhenElementFound()
    {
        var list = new List<int> { 1, 2, 3 };

        var result = list.FindOrError(x => x == 2);

        Assert.True(result.IsSuccess);
        Assert.Equal(2, result.Value);
    }

    [Fact]
    public void FindOrError_ShouldReturnCustomError_WhenElementNotFoundWithCustomError()
    {
        var list = new List<int> { 1, 2, 3 };

        var result = list.FindOrError(x => x == 4, "Custom error");

        Assert.False(result.IsSuccess);
        Assert.Equal("Custom error", result.Error);
    }

    [Fact]
    public void FindOrError_ShouldReturnElement_WhenElementFoundWithCustomError()
    {
        var list = new List<int> { 1, 2, 3 };

        var result = list.FindOrError(x => x == 2, "Custom error");

        Assert.True(result.IsSuccess);
        Assert.Equal(2, result.Value);
    }

    [Fact]
    public void FindOrError_ShouldReturnErrorFromFactory_WhenElementNotFound()
    {
        var list = new List<int> { 1, 2, 3 };

        var result = list.FindOrError(x => x == 4, () => "Generated error");

        Assert.False(result.IsSuccess);
        Assert.Equal("Generated error", result.Error);
    }

    [Fact]
    public void FindOrError_ShouldReturnElement_WhenElementFoundWithErrorFactory()
    {
        var list = new List<int> { 1, 2, 3 };

        var result = list.FindOrError(x => x == 2, () => "Generated error");

        Assert.True(result.IsSuccess);
        Assert.Equal(2, result.Value);
    }

    [Fact]
    public void FindOrMaybe_ShouldReturnSome_WhenElementFound()
    {
        var list = new List<int> { 1, 2, 3 };

        var result = list.MaybeFind(x => x == 2);

        Assert.True(result.HasValue);
        Assert.Equal(2, result.Value);
    }

    [Fact]
    public void FindOrMaybe_ShouldReturnNone_WhenElementNotFound()
    {
        var list = new List<int> { 1, 2, 3 };

        var result = list.MaybeFind(x => x == 4);

        Assert.False(result.HasValue);
    }

    [Fact]
    public void FindOrMaybe_ShouldReturnNone_WhenCollectionIsEmpty()
    {
        var list = new List<int>();

        var result = list.MaybeFind(x => x == 1);

        Assert.False(result.HasValue);
    }
}