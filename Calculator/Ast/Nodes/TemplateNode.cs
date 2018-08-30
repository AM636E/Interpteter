using System;
using System.Collections.Generic;

namespace Calculator.Ast.Nodes
{
    public class TemplateNode : AbstractSyntaxTree
    {
        private List<AbstractSyntaxTree> _arguments;
        private string _templateName;

        public TemplateNode(string templateName, List<AbstractSyntaxTree> arguments)
        {
            this._arguments = arguments;
            this._templateName = templateName;
        }

        public override double Accept(INodeVisitor visitor)
        {
            throw new NotImplementedException();
        }
    }
}
