namespace Calculator
{
    public abstract class AbstractSyntaxTree : IElement
    {
        public abstract double Accept(INodeVisitor visitor);
    }
}
