using Calculator.Nodes;

namespace Calculator.Ast.Nodes
{
    public enum TrigonometricType
    {
        Sin,
        Cos
    }

    public class TrigNode : UnaryNode
    {
        public TrigonometricType Type { get; }

        public TrigNode(Token op, TrigonometricType type, AbstractSyntaxTree abstractSyntaxTree) : base(op, abstractSyntaxTree)
        {
            Type = type;
        }

        public override double Accept(INodeVisitor visitor)
        {
            return visitor.VisitTrigonometric(this);
        }
    }
}
