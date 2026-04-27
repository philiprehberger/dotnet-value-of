using Xunit;

namespace Philiprehberger.ValueOf.Tests;

public class TryFromTests
{
    [Fact]
    public void TryFrom_ValidValue_ReturnsTrueAndInstance()
    {
        var success = PositiveInt.TryFrom(7, out var instance);

        Assert.True(success);
        Assert.NotNull(instance);
        Assert.Equal(7, instance!.Value);
    }

    [Fact]
    public void TryFrom_InvalidValue_ReturnsFalseAndNull()
    {
        var success = PositiveInt.TryFrom(-1, out var instance);

        Assert.False(success);
        Assert.Null(instance);
    }

    [Fact]
    public void TryFrom_NullReferenceValue_ReturnsFalse()
    {
        var success = NonEmptyString.TryFrom(null!, out var instance);

        Assert.False(success);
        Assert.Null(instance);
    }
}
