using System.Collections.Generic;
using System.Linq;

namespace Darwin.Syntax
{
    internal abstract record SyntaxNode
    {
        public virtual IEnumerable<SyntaxNode> GetChildren() => Enumerable.Empty<SyntaxNode>();
    }
}