using Calculator.Core.Ast;
using Calculator.Core.Ast.Nodes;
using Calculator.Core.Exceptions;
using Calculator.Core.Symbols;

namespace Calculator.Core
{
    /// <summary>
    /// Parses a string and return an AST.
    /// </summary>
    public class Parser
    {
        private Lexer _lexer;
        private Token _currentToken;

        public Parser(string text)
        {
            _lexer = new Lexer(text);
            _currentToken = _lexer.GetNextToken();
        }

        public AbstractSyntaxTree Parse()
        {
            return Expr();
        }

        public AbstractSyntaxTree Power()
        {
            var result = Factor();

            while (_currentToken.Type == TokenType.Pow)
            {
                var token = _currentToken;
                Eat(TokenType.Pow);

                result = new BinaryNode(result, token, Power());
            }

            return result;
        }

        /// <summary>
        /// Addition and Substraction.
        /// </summary>
        /// <returns></returns>
        public AbstractSyntaxTree Expr()
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

                result = new BinaryNode(result, token, Term());
            }

            return result;
        }

        /// <summary>
        /// Multiplication, Division and Power.
        /// </summary>
        /// <returns></returns>
        private AbstractSyntaxTree Term()
        {
            var result = Power();

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

                result = new BinaryNode(result, token, Factor());
            }

            return result;
        }

        /// <summary>
        /// Validate current token and move to the next.
        /// </summary>
        /// <param name="type"></param>
        private void Eat(TokenType type)
        {
            if (_currentToken.Type == type)
            {
                _currentToken = _lexer.GetNextToken();
            }
            else
            {
                throw new UnexpectedTokenException(type);
            }
        }

        /// <summary>
        /// Parse symbol expression.
        /// </summary>
        /// <returns></returns>
        private SymbolNode Symbol()
        {
            var result = _currentToken;
            Eat(TokenType.Var);
            return new SymbolNode(new Symbol(result.Value.ToString()));
        }

        /// <summary>
        /// Process factor expression.
        /// </summary>
        /// <returns></returns>
        private AbstractSyntaxTree Factor()
        {
            if (_currentToken.Type == TokenType.Number)
            {
                var result = _currentToken;
                Eat(TokenType.Number);
                return new NumberNode(result);
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
                return new UnaryNode(Token.Minus, Factor());
            }

            if (_currentToken.Type == TokenType.Minus)
            {
                Eat(TokenType.Plus);
                return new UnaryNode(Token.Plus, Factor());
            }

            if (_currentToken.Type == TokenType.Trig)
            {
                var token = _currentToken;
                Eat(TokenType.Trig);
                return new TrigNode(token, token.Value.Equals("sin") ? TrigonometricType.Sin : TrigonometricType.Cos, Factor());
            }

            if (_currentToken.Type == TokenType.Var)
            {
                var left = Symbol();
                var currentToken = _currentToken;
                if (currentToken.Type == TokenType.Assign)
                {
                    Eat(TokenType.Assign);
                    return new AssignNode(left, Expr());
                }

                return left;
            }

            throw new UnexpectedTokenException(_currentToken.Type);
        }
    }
}
