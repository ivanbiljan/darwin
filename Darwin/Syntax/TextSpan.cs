namespace Darwin.Syntax
{
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
            Start = start;
            Length = length;
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
}