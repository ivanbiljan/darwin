using System;
using Darwin.Syntax;

namespace Darwin.Interactive
{
    class Program
    {
        static void Main(string[] args)
        {
            var lexer = new Lexer("1 + 2+ 3");
            var tokens = lexer.TokenizeInput();
            var parser = new Parser(tokens);
            var tree = parser.Parse();
            Console.WriteLine(tree.Root);

            var eval = new Evaluator(tree.Root);
            Console.WriteLine(eval.Evaluate(tree.Root));
        }
    }
}