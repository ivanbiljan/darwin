using System.Collections.Generic;
using System.Linq;

namespace Darwin.Parser;

internal abstract record SyntaxNode
{
    public virtual IEnumerable<SyntaxNode> GetChildren()
    {
        return Enumerable.Empty<SyntaxNode>();
    }
}