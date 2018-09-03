using System.Collections.Generic;

namespace Calculator.Core.Symbols
{
    public class ScopedSymbolTable : SymbolTable
    {
        private readonly string _scopeName;
        private readonly int _scopeLevel;

        private readonly Dictionary<string, Symbol> SymbolNameToSymbol = new Dictionary<string, Symbol>();

        public ScopedSymbolTable(string scopeName, int scopeLevel)
        {
            _scopeName = scopeName;
            _scopeLevel = scopeLevel;
        }
    }
}
