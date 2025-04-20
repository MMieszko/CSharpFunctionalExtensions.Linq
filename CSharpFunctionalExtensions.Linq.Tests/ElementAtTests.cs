namespace CSharpFunctionalExtensions.Linq.Tests;

internal class ElementAtTests
{
    [Fact]
    public void ElementAtOrError_Result_ReturnsSuccess_WhenElementExists()
    {
        var source = new[] { 10, 20, 30 };
        var result = source.ElementAtOrError(1);

        Assert.True(result.IsSuccess);
        Assert.Equal(20, result.Value);
    }

    [Fact]
    public void ElementAtOrError_Result_ReturnsFailure_WhenIndexTooHigh()
    {
        var source = new[] { 10, 20 };
        var result = source.ElementAtOrError(2);

        Assert.True(result.IsFailure);
        Assert.Equal("Element not found", result.Error);
    }

    [Fact]
    public void ElementAtOrError_Result_ReturnsFailure_WhenIndexNegative()
    {
        var source = new[] { 1, 2 };
        var result = source.ElementAtOrError(-1);

        Assert.True(result.IsFailure);
    }

    [Fact]
    public void ElementAtOrError_Result_ReturnsFailure_WhenSourceIsEmpty()
    {
        var source = Array.Empty<string>();
        var result = source.ElementAtOrError(0);

        Assert.True(result.IsFailure);
    }

    [Fact]
    public void ElementAtOrError_ResultTE_ReturnsFailure_WhenIndexNegative()
    {
        var source = new[] { 1, 2 };
        var result = source.ElementAtOrError(-1, "err");

        Assert.True(result.IsFailure);
        Assert.Equal("err", result.Error);
    }

    [Fact]
    public void ElementAtOrError_ResultTE_ReturnsFailure_WhenSourceEmpty()
    {
        var source = Array.Empty<int>();
        var result = source.ElementAtOrError(0, "fail");

        Assert.True(result.IsFailure);
        Assert.Equal("fail", result.Error);
    }

    [Fact]
    public void ElementAtOrError_ResultTE_Works_WhenElementExists()
    {
        var source = new[] { 5, 6, 7 };
        var result = source.ElementAtOrError(2, "fail");

        Assert.True(result.IsSuccess);
        Assert.Equal(7, result.Value);
    }

    [Fact]
    public void ElementAtOrError_ErrorFactory_Called_WhenIndexNegative()
    {
        var source = new[] { "a", "b", "c" };
        var called = false;

        var result = source.ElementAtOrError(-1, () =>
        {
            called = true;
            return "bad index";
        });

        Assert.True(called);
        Assert.True(result.IsFailure);
        Assert.Equal("bad index", result.Error);
    }

    [Fact]
    public void ElementAtOrError_ErrorFactory_Called_WhenSourceEmpty()
    {
        var called = false;
        var result = Enumerable.Empty<string>().ElementAtOrError(0, () =>
        {
            called = true;
            return "empty";
        });

        Assert.True(called);
        Assert.True(result.IsFailure);
        Assert.Equal("empty", result.Error);
    }

    [Fact]
    public void MaybeElementAt_ReturnsSome_WhenElementExists()
    {
        var result = new[] { 1, 2, 3 }.MaybeElementAt(1);

        Assert.True(result.HasValue);
        Assert.Equal(2, result.Value);
    }

    [Fact]
    public void MaybeElementAt_ReturnsNone_WhenIndexTooHigh()
    {
        var result = new[] { 1, 2, 3 }.MaybeElementAt(3);

        Assert.True(result.HasNoValue);
    }

    [Fact]
    public void MaybeElementAt_ReturnsNone_WhenIndexIsNegative()
    {
        var result = new[] { 10, 20, 30 }.MaybeElementAt(-1);

        Assert.True(result.HasNoValue);
    }

    [Fact]
    public void MaybeElementAt_ReturnsNone_WhenSourceIsEmpty()
    {
        var result = Enumerable.Empty<int>().MaybeElementAt(0);

        Assert.True(result.HasNoValue);
    }

    [Fact]
    public void MaybeElementAt_WorksWithYieldedSource()
    {
        var result = Yielding(4).MaybeElementAt(2);

        Assert.True(result.HasValue);
        Assert.Equal(2, result.Value);
    }

    [Fact]
    public void ElementAtOrError_Result_WorksWithYieldedSource()
    {
        var result = Yielding(5).ElementAtOrError(3);

        Assert.True(result.IsSuccess);
        Assert.Equal(3, result.Value);
    }

    private static IEnumerable<int> Yielding(int count)
    {
        for (var i = 0; i < count; i++)
            yield return i;
    }
}