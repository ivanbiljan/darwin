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
            Console.WriteLine("Hello " + parser.ParseLiteral());
        }
    }
}