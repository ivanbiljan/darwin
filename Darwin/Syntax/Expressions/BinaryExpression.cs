using System.Collections.Generic;

namespace Darwin.Syntax.Expressions
{
    internal sealed record BinaryExpression(DarwinExpression LeftOperand, SyntaxToken Operator,
        DarwinExpression RightOperand) : DarwinExpression
    {
        public override DarwinExpressionType Type => DarwinExpressionType.Binary;

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield return LeftOperand;
            yield return Operator;
            yield return RightOperand;
        }
    }
}