using System;

namespace Darwin.Syntax;

/// <summary>
///     Represents a text span.
/// </summary>
public readonly struct TextSpan
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="TextSpan" /> struct with the specified start column and length.
    /// </summary>
    /// <param name="start">The start column.</param>
    /// <param name="length">The length.</param>
    public TextSpan(int start, int length)
    {
        if (start < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(start));
        }

        if (length < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(length));
        }

        Start = start;
        Length = length;
    }

    /// <summary>
    ///     Initializes a <see cref="TextSpan" /> instance from the given bounds.
    /// </summary>
    /// <param name="start">The start column.</param>
    /// <param name="end">The end column.</param>
    /// <returns>The <see cref="TextSpan" /> that wraps the given bounds.</returns>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     <paramref name="start" /> is negative or <paramref name="end" /> is &lt;=
    ///     <paramref name="start" />.
    /// </exception>
    public static TextSpan FromBounds(int start, int end)
    {
        if (start < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(start));
        }

        if (end <= start)
        {
            throw new ArgumentOutOfRangeException(nameof(end));
        }

        return new TextSpan(start, end - start);
    }

    /// <summary>
    ///     Gets the start column.
    /// </summary>
    public int Start { get; }

    /// <summary>
    ///     Gets the span length.
    /// </summary>
    public int Length { get; }
}