namespace Calculator
{
    public interface IElement
    {
        double Accept(INodeVisitor visitor);
    }
}
