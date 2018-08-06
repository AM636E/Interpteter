namespace Calculator.Nodes
{
    public class BinaryNode : AbstractSyntaxTree
    {
        public AbstractSyntaxTree Left { get; set; }
        public Token Op { get; set; }
        public AbstractSyntaxTree Right { get; set; }

        public BinaryNode(AbstractSyntaxTree left, Token op, AbstractSyntaxTree right)
        {
            Left = left;
            Op = op;
            Right = right;
        }

        public override double Accept(INodeVisitor visitor)
        {
            return visitor.VisitBinaryOp(this);
        }
    }
}
