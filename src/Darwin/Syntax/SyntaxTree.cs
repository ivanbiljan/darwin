using Darwin.LexicalAnalysis;

namespace Darwin.Syntax;

internal sealed class SyntaxTree
{
    public SyntaxTree(SyntaxNode root, SyntaxToken endOfFile)
    {
        Root = root;
        EndOfFile = endOfFile;
    }

    public SyntaxToken EndOfFile { get; }

    public SyntaxNode Root { get; }
}