namespace Calculator.Ui.Wpf.Model
{
    public class SymbolItem : NotifyPropertyChangedBase
    {
        private string _name;
        private string _value;

        public string Name { get => _name; set => ChangeProperty(() => _name = value, nameof(Name)); }
        public string Value { get => _value; set => ChangeProperty(() => _value = value, nameof(Value)); }
    }
}
