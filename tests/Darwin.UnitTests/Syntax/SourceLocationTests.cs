using System;
using Darwin.Syntax;
using Xunit;

namespace Darwin.UnitTests.Syntax
{
    public sealed class SourceLocationTests
    {
        [Fact]
        public void Ctor_NegativeLineNumber_ThrowsArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new SourceLocation(-1, default));
        }

        [Fact]
        public void Ctor_IsCorrect()
        {
            var sourceLocation = new SourceLocation(1, new TextSpan(2, 3));
            
            Assert.Equal(1, sourceLocation.Line);
            Assert.Equal(new TextSpan(2, 3), sourceLocation.TextSpan);
        }
    }
}