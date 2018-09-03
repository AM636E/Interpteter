namespace Calculator.Core.Ast
{
    public interface IElement
    {
        double Accept(INodeVisitor visitor);
    }
}
