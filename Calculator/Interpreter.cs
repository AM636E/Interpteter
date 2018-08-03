using System;

namespace Calculator
{
    public interface INodeVisitor
    {
        double Visit(Ast ast);
        double VisitBinaryOp(BinaryOperation operation);
        double VisitNumber(Number number);
        double VisitUnary(UnaryOperation unaryOperation);
    }

    public interface IElement
    {
        double Accept(INodeVisitor visitor);
    }

    public abstract class Ast : IElement
    {
        public abstract double Accept(INodeVisitor visitor);
    }

    public class UnaryOperation : Ast
    {
        public Token Op { get; }
        public Ast Expr { get; }

        public UnaryOperation(Token op, Ast expr)
        {
            Op = op;
            Expr = expr;
        }

        public override double Accept(INodeVisitor visitor)
        {
            return visitor.VisitUnary(this);
        }
    }

    public class BinaryOperation : Ast
    {
        public Ast Left { get; set; }
        public Token Op { get; set; }
        public Ast Right { get; set; }

        public BinaryOperation(Ast left, Token op, Ast right)
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

    public class Number : Ast
    {
        private readonly Token token;

        public Number(Token token)
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

    public class Interpreter : INodeVisitor
    {
        private Token _currentToken;
        private Lexer _lexer;

        public Interpreter(string text)
        {
            _lexer = new Lexer(text);
            _currentToken = _lexer.GetNextToken();
        }

        public Ast Parse()
        {
            return Expr();
        }

        public Ast Expr()
        {
            var result = Term();

            while (_currentToken.Type == TokenType.Plus || _currentToken.Type == TokenType.Minus)
            {
                var token = _currentToken;
                if (_currentToken.Type == TokenType.Plus)
                {
                    Eat(TokenType.Plus);
                }
                else if (_currentToken.Type == TokenType.Minus)
                {
                    Eat(TokenType.Minus);
                }

                result = new BinaryOperation(result, token, Term());
            }

            return result;
        }

        private Ast Term()
        {
            var result = Factor();

            while (_currentToken.Type == TokenType.Mul || _currentToken.Type == TokenType.Div)
            {
                var token = _currentToken;
                if (_currentToken.Type == TokenType.Mul)
                {
                    Eat(TokenType.Mul);
                }
                else if (_currentToken.Type == TokenType.Div)
                {
                    Eat(TokenType.Div);
                }

                result = new BinaryOperation(result, token, Factor());
            }

            return result;
        }

        private void Eat(TokenType type)
        {
            if (_currentToken.Type == type)
            {
                _currentToken = _lexer.GetNextToken();
            }
            else
            {
                throw new Exception();
            }
        }

        private Ast Factor()
        {
            if (_currentToken.Type == TokenType.Number)
            {
                var result = _currentToken;
                Eat(TokenType.Number);
                return new Number(result);
            }

            if (_currentToken.Type == TokenType.Lp)
            {
                Eat(TokenType.Lp);
                var result = Expr();
                Eat(TokenType.Rp);
                return result;
            }

            if (_currentToken.Type == TokenType.Minus)
            {
                Eat(TokenType.Minus);
                return new UnaryOperation(Token.Minus, Factor());
            }

            if (_currentToken.Type == TokenType.Minus)
            {
                Eat(TokenType.Plus);
                return new UnaryOperation(Token.Plus, Factor());
            }

            if (_currentToken.Type == TokenType.Sin)
            {
                Eat(TokenType.Sin);
                return new UnaryOperation(Token.Sin, Factor());
            }

            throw new Exception();
        }

        public double Visit(Ast ast)
        {
            return ast.Accept(this);
        }

        public double VisitBinaryOp(BinaryOperation operation)
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
            }

            throw new Exception();
        }

        public double VisitNumber(Number number)
        {
            return number.Value;
        }

        public double Interpter()
        {
            var ast = Expr();

            return Visit(ast);
        }

        public double VisitUnary(UnaryOperation unaryOperation)
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
                case TokenType.Sin:
                    {
                        return Math.Sin(Visit(unaryOperation.Expr));
                    }
            }

            throw new Exception();
        }
    }
}
