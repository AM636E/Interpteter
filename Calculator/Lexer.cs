using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator
{
    public class Lexer
    {
        private string text;
        private int position;
        private char? currentChar;

        public Lexer(string text)
        {
            this.text = text;
            currentChar = text[position];
        }

        private void SkipWhitespace()
        {
            while (currentChar.HasValue && char.IsWhiteSpace(currentChar.Value))
            {
                Advance();
            }
        }

        private double Number()
        {
            var result = string.Empty;
            while (currentChar.HasValue && (char.IsDigit(currentChar.Value) || currentChar == '.'))
            {
                result += currentChar;
                Advance();
            }

            return double.Parse(result);
        }

        private void Advance()
        {
            Advance(1);
        }

        private string Peek(int num)
        {
            if (position + num > text.Length)
            {
                return null;
            }

            return text.Substring(position, num);
        }

        private void Advance(int n)
        {
            position += n;

            if (position >= text.Length)
            {
                currentChar = null;
            }
            else
            {
                currentChar = text[position];
            }
        }

        public Token GetNextToken()
        {
            while (currentChar.HasValue)
            {
                if (char.IsWhiteSpace(currentChar.Value))
                {
                    SkipWhitespace();
                    continue;
                }

                if (char.IsDigit(currentChar.Value))
                {
                    return new Token { Type = TokenType.Number, Value = Number() };
                }

                if (currentChar == '+')
                {
                    Advance();
                    return Token.Plus;
                }

                if (currentChar == '-')
                {
                    Advance();
                    return Token.Minus;
                }

                if (currentChar == '*')
                {
                    Advance();
                    return Token.Mul;
                }

                if (currentChar == '/')
                {
                    Advance();
                    return Token.Div;
                }

                if (currentChar == '(')
                {
                    Advance();
                    return new Token { Type = TokenType.Lp, Value = '(' };
                }

                if (currentChar == ')')
                {
                    Advance();
                    return new Token { Type = TokenType.Rp, Value = ')' };
                }

                if (Peek(3) == "sin")
                {
                    Advance(3);
                    return Token.Sin;
                }

                if (Peek(2) == "PI")
                {
                    Advance(2);
                    return new Token { Type = TokenType.Number, Value = Math.PI };
                }
            }

            return new Token { Type = TokenType.End };
        }
    }
}
