using Calculator.Ast.Nodes;
using Calculator.Nodes;
using Calculator.Symbols;
using System;
using System.Collections.Generic;

namespace Calculator
{
    public class Interpreter : INodeVisitor
    {
        private Parser _parser;
        private static SymbolTable _symbolTable = new SymbolTable();
        private static Dictionary<string, double> SymbolNameToValue = new Dictionary<string, double>
        {
            ["PI"] = Math.PI
        };

        public Interpreter(string text)
        {
            _parser = new Parser(text);
        }

        public double Visit(AbstractSyntaxTree ast)
        {
            return ast.Accept(this);
        }

        public double VisitBinaryOp(BinaryNode operation)
        {
            var left = Visit(operation.Left);
            var right = Visit(operation.Right);

            switch (operation.Op.Type)
            {
                case TokenType.Div:
                    {
                        return left / right;
                    }
                case TokenType.Mul:
                    {
                        return left * right;
                    }
                case TokenType.Minus:
                    {
                        return left - right;
                    }
                case TokenType.Plus:
                    {
                        return left + right;
                    }
                case TokenType.Pow:
                    {
                        return Math.Pow(left, right);
                    }
            }

            throw new Exception();
        }

        public double VisitNumber(NumberNode number)
        {
            return number.Value;
        }

        public double Interpter()
        {
            var ast = _parser.Parse();

            return Visit(ast);
        }

        public double VisitUnary(UnaryNode unaryOperation)
        {
            switch (unaryOperation.Op.Type)
            {
                case TokenType.Minus:
                    {
                        return -Visit(unaryOperation.Expr);
                    }
                case TokenType.Plus:
                    {
                        return Visit(unaryOperation.Expr);
                    }
            }

            throw new Exception();
        }

        public double VisitTrigonometric(TrigNode trigonometricOperation)
        {
            switch (trigonometricOperation.Type)
            {
                case TrigonometricType.Sin:
                    {
                        return Math.Sin(Visit(trigonometricOperation.Expr));
                    }
                case TrigonometricType.Cos:
                    {
                        return Math.Cos(Visit(trigonometricOperation.Expr));
                    }
            }

            throw new Exception();
        }

        public double VisitVar(SymbolNode symbolNode)
        {
            if (!_symbolTable.HasSymbol(symbolNode.Symbol.Name))
            {
                throw new Exception($"Unknown symbol: {symbolNode.Symbol.Name}.");
            }

            if (SymbolNameToValue.ContainsKey(symbolNode.Symbol.Name))
            {
                return SymbolNameToValue[symbolNode.Symbol.Name];
            }

            return 0;
        }

        public double VisitAssign(AssignNode assignNode)
        {
            _symbolTable.Define(assignNode.Symbol.Symbol);
            return SymbolNameToValue[assignNode.Symbol.Symbol.Name] = Visit(assignNode.Expr);
        }
    }
}
