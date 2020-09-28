namespace Darwin.CodeAnalysis {
    internal sealed class Lexer {
        private readonly string _input;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="Lexer"/> class with the specified input string.
        /// </summary>
        /// <param name="input">The input string.</param>
        public Lexer(string input) {
            _input = input;
        }
    }
}