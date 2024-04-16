using System;

namespace Darwin.LexicalAnalysis;

/// <summary>
///     Represents a struct that contains a token's location information.
/// </summary>
public readonly struct SourceLocation
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="SourceLocation" /> struct with the specified line number and text
    ///     span.
    /// </summary>
    /// <param name="line">The line number.</param>
    /// <param name="textSpan">The text span.</param>
    public SourceLocation(int line, TextSpan textSpan)
    {
        if (line < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(line));
        }

        Line = line;
        TextSpan = textSpan;
    }

    /// <summary>
    ///     Gets the line number the token is defined at.
    /// </summary>
    public int Line { get; }

    /// <summary>
    ///     Gets the token's text span.
    /// </summary>
    public TextSpan TextSpan { get; }
}