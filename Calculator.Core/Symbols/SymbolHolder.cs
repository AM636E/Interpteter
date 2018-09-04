using Calculator.Core.Exceptions;
using System;
using System.Collections.Generic;

namespace Calculator.Core.Symbols
{
    public class SymbolHolder
    {
        private readonly SymbolTable _table;

        private readonly SymbolTable _symbolTable;

        private Dictionary<string, double> _symbolNameToValue { get; } = new Dictionary<string, double>
        {
            ["PI"] = Math.PI,
            ["E"] = Math.E
        };

        public IReadOnlyDictionary<string, double> SymbolDictionary => _symbolNameToValue;

        public SymbolHolder(SymbolTable table)
        {
            _table = table;
        }

        public void Define(Symbol symbol)
        {
            _table.Define(symbol);
        }

        public void Set(Symbol symbol, double value)
        {
            if (_table.HasSymbol(symbol.Name))
            {
                _symbolNameToValue[symbol.Name] = value;
            }
            else
            {
                throw new UnknownSymbolException(symbol.Name);
            }
        }

        public double Get(string symbolName)
        {
            if (_table.HasSymbol(symbolName))
            {
                return _symbolNameToValue[symbolName];
            }
            else
            {
                throw new UnknownSymbolException(symbolName);
            }
        }

        public bool HasSymbol(string name)
        {
            return _table.HasSymbol(name);
        }

        public static SymbolHolder Default => new SymbolHolder(new SymbolTable());
    }
}
