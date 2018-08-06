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
        ExprEnd,
        Out,
        Var,
        Assign,
        Compare,
        Pow,
        Trig
    }

    public class Token
    {
        public TokenType Type { get; set; }

        public object Value { get; set; }

        public static Token Mul => new Token { Type = TokenType.Mul, Value = "*" };
        public static Token Div => new Token { Type = TokenType.Div, Value = "/" };
        public static Token Plus => new Token { Type = TokenType.Plus, Value = "+" };
        public static Token Minus => new Token { Type = TokenType.Minus, Value = "-" };

        public static Token Pow => new Token { Type = TokenType.Pow, Value = '^' };

        public static Token Assign => new Token { Type = TokenType.Assign, Value = '=' };

        public static Token Trig(string type) => new Token { Type = TokenType.Trig, Value = type };

        public override string ToString()
        {
            return $"T:[{Type}]|V={Value}";
        }
    }
}
