using System.Collections.Generic;

namespace Calculator.Symbols
{
    public class TemplateSymbol : Symbol
    {
        private List<Symbol> _arguments;

        public TemplateSymbol(string templateName, List<Symbol> arguments = null) : base(templateName)
        {
            _arguments = arguments;
        }
    }
}
