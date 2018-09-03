using Calculator.Core.Symbols;

namespace Calculator.Core.Ast.Nodes
{
    public class SymbolNode : AbstractSyntaxTree
    {
        public Symbol Symbol { get; }

        public SymbolNode(Symbol symbol)
        {
            Symbol = symbol;
        }

        public override double Accept(INodeVisitor visitor)
        {
            return visitor.VisitVar(this);
        }
    }
}
