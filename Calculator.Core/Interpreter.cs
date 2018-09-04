using Calculator.Core.Ast;
using Calculator.Core.Ast.Nodes;
using Calculator.Core.Exceptions;
using Calculator.Core.Symbols;
using System;
using System.Collections.Generic;

namespace Calculator.Core
{
    /// <summary>
    /// Interprets AST.
    /// Every operation represented as a node. 
    /// Interpretation is done by visiting every node.
    /// </summary>
    public class Interpreter : INodeVisitor
    {
        private Parser _parser;
        private static SymbolTable _symbolTable = new SymbolTable();
        private static Dictionary<string, double> SymbolNameToValue = new Dictionary<string, double>
        {
            ["PI"] = Math.PI,
            ["E"] = Math.E
        };

        public static Dictionary<string, double> SymbolDictionary => SymbolNameToValue;

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
                        if (right == 0)
                        {
                            throw new ZeroDivizionException();
                        }
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

        public double Interpret()
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

            throw new UnexpectedOperationException(unaryOperation.Op);
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

            throw new UnexpectedOperationException(trigonometricOperation.Op);
        }

        /// <summary>
        /// Verifies if there is a symbol and returns it value.
        /// </summary>
        /// <param name="symbolNode"></param>
        /// <returns></returns>
        public double VisitVar(SymbolNode symbolNode)
        {
            if (!_symbolTable.HasSymbol(symbolNode.Symbol.Name))
            {
                throw new UnknownSymbolException($"{symbolNode.Symbol.Name}");
            }

            if (SymbolNameToValue.ContainsKey(symbolNode.Symbol.Name))
            {
                return SymbolNameToValue[symbolNode.Symbol.Name];
            }

            throw new UnknownSymbolException(symbolNode.Symbol.Name);
        }

        /// <summary>
        /// Defines a symbol in symbol table and computes its value.
        /// </summary>
        /// <param name="assignNode"></param>
        /// <returns></returns>
        public double VisitAssign(AssignNode assignNode)
        {
            _symbolTable.Define(assignNode.Symbol.Symbol);
            return SymbolNameToValue[assignNode.Symbol.Symbol.Name] = Visit(assignNode.Expr);
        }
    }
}
