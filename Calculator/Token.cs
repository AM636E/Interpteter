namespace Calculator
{
    public enum TokenType
    {
        Number,
        Plus,
        End,
        Minus,
        Mul,
        Div,
        Lp,
        Rp,
        Sin,
        ExprEnd,
        Out,
        Var,
        Assign,
        Compare
    }

    public class Token
    {
        public TokenType Type { get; set; }

        public object Value { get; set; }

        public static Token Mul => new Token { Type = TokenType.Mul, Value = "*" };
        public static Token Div => new Token { Type = TokenType.Div, Value = "/" };
        public static Token Plus => new Token { Type = TokenType.Plus, Value = "+" };
        public static Token Minus => new Token { Type = TokenType.Minus, Value = "-" };

        public static Token Sin => new Token { Type = TokenType.Sin, Value = "sin" };

        public override string ToString()
        {
            return $"T:[{Type}]|V={Value}";
        }
    }
}
