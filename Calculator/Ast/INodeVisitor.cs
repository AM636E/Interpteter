using Calculator.Ast.Nodes;
using Calculator.Nodes;

namespace Calculator
{
    public interface INodeVisitor
    {
        double Visit(AbstractSyntaxTree ast);
        double VisitBinaryOp(BinaryNode operation);
        double VisitNumber(NumberNode number);
        double VisitUnary(UnaryNode unaryOperation);
        double VisitTrigonometric(TrigNode trigonometricOperation);
        double VisitVar(SymbolNode symbolNode);
        double VisitAssign(AssignNode assignNode);
    }
}
