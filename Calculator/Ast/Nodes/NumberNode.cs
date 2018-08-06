namespace Calculator.Nodes
{
    public class NumberNode : AbstractSyntaxTree
    {
        private readonly Token token;

        public NumberNode(Token token)
        {
            this.token = token;
            Value = (token.Value as double?).Value;
        }

        public double Value { get; }

        public override double Accept(INodeVisitor visitor)
        {
            return visitor.VisitNumber(this);
        }

        public override string ToString()
        {
            return token.ToString();
        }
    }
}
