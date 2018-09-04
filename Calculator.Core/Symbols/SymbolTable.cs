using Calculator.Core.Exceptions;
using System.Collections.Generic;

namespace Calculator.Core.Symbols
{
    public class SymbolTable
    {
        private Dictionary<string, Symbol> _nameToSymbol = new Dictionary<string, Symbol>
        {
            ["PI"] = new BuiltInSymbol("PI"),
            ["E"] = new BuiltInSymbol("E"),
            ["sin"] = new BuiltInSymbol("sin"),
            ["cos"] = new BuiltInSymbol("cos"),
        };

        public bool HasSymbol(string name)
        {
            return _nameToSymbol.ContainsKey(name);
        }

        public void Define(Symbol symbol)
        {
            if (_nameToSymbol.ContainsKey(symbol.Name) && _nameToSymbol[symbol.Name] is BuiltInSymbol)
            {
                throw new ReservedSymbolException(symbol.Name);
            }
            _nameToSymbol[symbol.Name] = symbol;
        }

        public Symbol Lookup(string name)
        {
            return _nameToSymbol[name];
        }
    }
}
