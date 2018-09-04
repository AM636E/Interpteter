using Calculator.Core;
using Calculator.Core.Exceptions;
using Calculator.Core.Symbols;
using Xunit;

namespace Calculator.Tests
{
    public class InterpreterTests
    {
        [Theory]
        [InlineData("2+2", 4)]
        [InlineData("(2+2)", 4)]
        [InlineData("((2)+(2))", 4)]
        [InlineData("2+2*2", 6)]
        [InlineData("(2+2)*2", 8)]
        [InlineData("2-2", 0)]
        [InlineData("2^2", 4)]
        [InlineData("2^2^3", 256)]
        [InlineData("2^(1+1)^(2+1)", 256)]
        [InlineData("2^(1*2)^(1.5*2)", 256)]
        [InlineData("1.5*2", 3)]
        [InlineData("1^1/2", 0.5)]
        [InlineData("0.5+1^1/2", 1)]
        public void TestExpressions(string expression, double expected)
        {
            var interpteter = new Interpreter(expression);
            var actual = interpteter.Interpret();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestZeroDivision()
        {
            var interpreter = new Interpreter("40/(30-30)");

            Assert.Throws<ZeroDivizionException>(() => interpreter.Interpret());
        }

        [Theory]
        [InlineData("2-(4+1")]
        [InlineData("2-")]
        [InlineData("2-2*")]
        public void InvalidExpressionException(string expression)
        {
            var interpreter = new Interpreter(expression);
            Assert.Throws<UnexpectedTokenException>(() => interpreter.Interpret());
        }

        [Fact]
        public void TestWithSymbols()
        {
            var table = new SymbolTable();
            var holder = new SymbolHolder(table);
            var interpreter = new Interpreter("a=2+2", holder);
            interpreter.Interpret();

            Assert.True(table.HasSymbol("a"));
            Assert.Equal(4, holder.Get("a"));

            interpreter = new Interpreter("b=a+a", holder);
            interpreter.Interpret();
            Assert.True(table.HasSymbol("b"));
            Assert.Equal(8, holder.Get("b"));
        }

        [Fact]
        public void TestWithUnknownSymbols()
        {
            var table = new SymbolTable();
            var holder = new SymbolHolder(table);
            var interpreter = new Interpreter("a", holder);

            Assert.Throws<UnknownSymbolException>(() =>
            {
                interpreter.Interpret();
            });
        }

        [Fact]
        public void TestWithReservedSymbols()
        {
            var table = new SymbolTable();
            var holder = new SymbolHolder(table);
            var interpreter = new Interpreter("PI=3", holder);

            Assert.Throws<ReservedSymbolException>(() =>
            {
                interpreter.Interpret();
            });
        }
    }
}
