namespace Calculator.Ui.Wpf.Model
{
    public class HistoryItem : NotifyPropertyChangedBase
    {
        private string _expression;
        private string _value;

        public string Expression { get => _expression; set => ChangeProperty(() => _expression = value, nameof(Expression)); }
        public string Value { get => _value; set => ChangeProperty(() => _value = value, nameof(Value)); }
    }
}
