using System.Collections.Generic;

namespace Calculator.Symbols
{
    public class SymbolTable
    {
        private Dictionary<string, Symbol> _nameToSymbol = new Dictionary<string, Symbol>
        {
            ["PI"] = new BuiltInSymbol("PI"),
            ["sin"] = new BuiltInSymbol("sin"),
            ["cos"] = new BuiltInSymbol("cos"),
        };

        public bool HasSymbol(string name)
        {
            return _nameToSymbol.ContainsKey(name);
        }

        public void Define(Symbol symbol)
        {
            _nameToSymbol[symbol.Name] = symbol;
        }

        public Symbol Lookup(string name)
        {
            return _nameToSymbol[name];
        }
    }
}
