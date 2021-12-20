using System;
using Darwin.Syntax;

namespace Darwin.Interactive
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                var input = Console.ReadLine();
                if (input == "exit")
                {
                    return;
                }
                
                var lexer = new Lexer(input);
                var tokens = lexer.TokenizeInput();
                var parser = new Parser(tokens);
                
                var tree = parser.Parse();
                Console.WriteLine(tree.Root);

                var eval = new Evaluator();
                Console.WriteLine(eval.Evaluate(tree.Root));
            }
        }
    }
}