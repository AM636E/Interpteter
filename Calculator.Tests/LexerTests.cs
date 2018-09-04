using Calculator.Core;
using System.Collections.Generic;
using Xunit;
namespace Calculator.Tests
{
    public class LexerTests
    {
        [Fact]
        public void TestTokenizing()
        {
            var lexer = new Lexer("222.5+(2-3)  *  2 / 0 - sin 5 * cos 4");
            var token = lexer.GetNextToken();
            var tokens = new List<Token>();

            while (token.Type != TokenType.End)
            {
                tokens.Add(token);
                token = lexer.GetNextToken();
            }

            Assert.Equal(17, tokens.Count);
            Assert.Equal(TokenType.Number, tokens[0].Type);
            Assert.Equal(222.5, tokens[0].Value);

            Assert.Equal(TokenType.Plus, tokens[1].Type);
            Assert.Equal(TokenType.Lp, tokens[2].Type);

            Assert.Equal(TokenType.Number, tokens[3].Type);
            Assert.Equal(2.0, tokens[3].Value);

            Assert.Equal(TokenType.Minus, tokens[4].Type);

            Assert.Equal(TokenType.Number, tokens[5].Type);
            Assert.Equal(3.0, tokens[5].Value);

            Assert.Equal(TokenType.Rp, tokens[6].Type);

            Assert.Equal(TokenType.Mul, tokens[7].Type);

            Assert.Equal(TokenType.Number, tokens[8].Type);
            Assert.Equal(2.0, tokens[8].Value);

            Assert.Equal(TokenType.Div, tokens[9].Type);

            Assert.Equal(TokenType.Number, tokens[10].Type);
            Assert.Equal(0.0, tokens[10].Value);

            Assert.Equal(TokenType.Minus, tokens[11].Type);

            Assert.Equal(TokenType.Trig, tokens[12].Type);

            Assert.Equal(TokenType.Number, tokens[13].Type);
            Assert.Equal(5.0, tokens[13].Value);

            Assert.Equal(TokenType.Mul, tokens[14].Type);

            Assert.Equal(TokenType.Trig, tokens[15].Type);

            Assert.Equal(TokenType.Number, tokens[16].Type);
            Assert.Equal(4.0, tokens[16].Value);
        }
    }
}
