using Calculator.Core;
using Calculator.Core.Ast.Nodes;
using Xunit;

namespace Calculator.Tests
{
    public class ParserTests
    {
        [Fact]
        public void TestSimple()
        {
            var parser = new Parser("2+2");
            var tree = parser.Parse();

            Assert.IsType<BinaryNode>(tree);

            var binaryNode = tree as BinaryNode;

            Assert.IsType<NumberNode>(binaryNode.Left);
            Assert.Equal(TokenType.Plus, binaryNode.Op.Type);
            Assert.IsType<NumberNode>(binaryNode.Right);

            var left = binaryNode.Left as NumberNode;
            var right = binaryNode.Right as NumberNode;

            Assert.Equal(2, left.Value);
            Assert.Equal(2, right.Value);
        }

        [Fact]
        public void TestWithPrioritySimple()
        {
            var parser = new Parser("(2+3)*4");
            var tree = parser.Parse();

            Assert.IsType<BinaryNode>(tree);

            var binaryNode = tree as BinaryNode;

            Assert.IsType<BinaryNode>(binaryNode.Left);
            Assert.Equal(TokenType.Mul, binaryNode.Op.Type);
            Assert.IsType<NumberNode>(binaryNode.Right);

            var left = binaryNode.Left as BinaryNode;
            var right = binaryNode.Right as NumberNode;

            var leftLeft = left.Left as NumberNode;
            var leftRight = left.Right as NumberNode;
            Assert.Equal(TokenType.Plus, left.Op.Type);

            Assert.Equal(2, leftLeft.Value);
            Assert.Equal(3, leftRight.Value);

            Assert.Equal(4, right.Value);
        }

        [Fact]
        public void TestWithPriorityPower()
        {
            var parser = new Parser("2^3^4");
            var tree = parser.Parse();

            Assert.IsType<BinaryNode>(tree);

            var binaryNode = tree as BinaryNode;

            Assert.IsType<NumberNode>(binaryNode.Left);
            Assert.Equal(TokenType.Pow, binaryNode.Op.Type);
            Assert.IsType<BinaryNode>(binaryNode.Right);

            var left = binaryNode.Left as NumberNode;
            var right = binaryNode.Right as BinaryNode;

            var rightLeft = right.Left as NumberNode;
            var rightRight = right.Right as NumberNode;
            Assert.Equal(TokenType.Pow, right.Op.Type);

            Assert.Equal(3, rightLeft.Value);
            Assert.Equal(4, rightRight.Value);

            Assert.Equal(2, left.Value);
        }
    }
}
