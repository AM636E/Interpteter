using Calculator.Symbols;

namespace Calculator.Ast.Nodes
{
    public class AssignNode : AbstractSyntaxTree
    {
        public AbstractSyntaxTree Expr { get; }
        public SymbolNode Symbol { get; }

        public AssignNode(SymbolNode symbol, AbstractSyntaxTree expr)
        {
            Symbol = symbol;
            Expr = expr;
        }

        public override double Accept(INodeVisitor visitor)
        {
            return visitor.VisitAssign(this);
        }
    }
}
