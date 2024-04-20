using System;
using Darwin.Parser;
using Xunit;

namespace Darwin.UnitTests.Syntax;

public sealed class TextSpanTests
{
    [Fact]
    public void Ctor_NegativeStart_ThrowsArgumentOutOfRangeException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new TextSpan(-1, 0));
    }

    [Fact]
    public void Ctor_NegativeLength_ThrowsArgumentOutOfRangeException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new TextSpan(0, -1));
    }

    [Fact]
    public void Ctor_IsCorrect()
    {
        var textSpan = new TextSpan(0, 3);

        Assert.Equal(0, textSpan.Start);
        Assert.Equal(3, textSpan.Length);
    }

    [Fact]
    public void FromBounds_NegativeStart_ThrowsArgumentOutOfRangeException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => TextSpan.FromBounds(-1, 0));
    }

    [Fact]
    public void FromBounds_EndLessThanStart_ThrowsArgumentOutOfRangeException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => TextSpan.FromBounds(2, 1));
    }

    [Fact]
    public void FromBounds_IsCorrect()
    {
        var textSpan = TextSpan.FromBounds(2, 5);

        Assert.Equal(2, textSpan.Start);
        Assert.Equal(3, textSpan.Length);
    }
}