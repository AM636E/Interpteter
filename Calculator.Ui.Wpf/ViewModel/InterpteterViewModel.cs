using Calculator.Core;
using Calculator.Core.Symbols;
using Calculator.Ui.Wpf.Collections;
using Calculator.Ui.Wpf.Commands;
using Calculator.Ui.Wpf.Model;
using System;
using System.Windows.Input;

namespace Calculator.Ui.Wpf.ViewModel
{
    public class InterpteterViewModel : NotifyPropertyChangedBase
    {
        private string _expression;
        private string _currentValue;
        private bool _isValid;

        public SymbolHolder SymbolHolder { get; private set; }

        public CustomObservableCollection<HistoryItem> HistoryItems { get; set; } = new CustomObservableCollection<HistoryItem>(true);
        public ObservableSymbolCollection SymbolItems { get; set; }

        public string Expression
        {
            get => _expression;
            set
            {
                _expression = value;
                CurrentValue = InterpretCurrent(value);
                OnPropertyChanged(nameof(Expression));
            }
        }

        public string CurrentValue
        {
            get => _currentValue;
            set
            {
                _currentValue = value;
                OnPropertyChanged(nameof(CurrentValue));
            }
        }

        public bool IsValid
        {
            get => _isValid;
            set
            {
                if (_isValid == value) { return; }
                _isValid = value;
                OnPropertyChanged(nameof(IsValid));
            }
        }

        private string InterpretCurrent(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }
            try
            {
                var result = new Interpreter(text, SymbolHolder).Interpret().ToString();
                SymbolItems.Update();
                IsValid = true;
                return result;
            }
            catch (Exception ex)
            {
                IsValid = false;
                return ex.Message;
            }
        }

        public InterpteterViewModel()
        {
            SymbolHolder = new SymbolHolder(new SymbolTable());
            SymbolItems = new ObservableSymbolCollection(SymbolHolder);
            CurrentValue = "(Start typing)";
            Expression = "2+2";
        }

        public ICommand InterpterCommand => new InterpetCommand(this);
        public ICommand AppendCommand => new AppendCommand(this);
    }
}
