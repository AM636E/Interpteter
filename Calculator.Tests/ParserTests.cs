using Calculator.Nodes;
using System;
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
    }
}
