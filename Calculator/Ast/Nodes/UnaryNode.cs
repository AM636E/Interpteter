namespace Calculator.Nodes
{
    public class UnaryNode : AbstractSyntaxTree
    {
        public Token Op { get; }
        public AbstractSyntaxTree Expr { get; }

        public UnaryNode(Token op, AbstractSyntaxTree expr)
        {
            Op = op;
            Expr = expr;
        }

        public override double Accept(INodeVisitor visitor)
        {
            return visitor.VisitUnary(this);
        }
    }
}
