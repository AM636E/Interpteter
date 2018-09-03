using Calculator.Core;
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
        public void TestExpressions(string expression, double expected)
        {
            var interpteter = new Interpreter(expression);
            var actual = interpteter.Interpret();
            Assert.Equal(expected, actual);
        }
    }
}
