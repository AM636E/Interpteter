namespace Calculator.Core.Symbols
{
    public class Symbol
    {
        public Symbol(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public override string ToString()
        {
            return $"<Symbol>:{Name}";
        }
    }
}
