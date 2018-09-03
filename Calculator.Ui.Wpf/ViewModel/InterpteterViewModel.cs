using Calculator.Core;
using Calculator.Ui.Wpf.Collections;
using Calculator.Ui.Wpf.Commands;
using Calculator.Ui.Wpf.Model;
using System.Windows.Input;

namespace Calculator.Ui.Wpf.ViewModel
{
    public class InterpteterViewModel : NotifyPropertyChangedBase
    {
        private string _expression;

        public CustomObservableCollection<HistoryItem> HistoryItems { get; set; } = new CustomObservableCollection<HistoryItem>(true);
        public ObservableSymbolCollection SymbolItems { get; set; } = new ObservableSymbolCollection(Interpreter.SymbolDictionary);

        public string Expression
        {
            get => _expression;
            set
            {
                _expression = value;
                OnPropertyChanged(nameof(Expression));
            }
        }

        public InterpteterViewModel()
        {
        }

        public ICommand InterpterCommand => new InterpetCommand(this);
        public ICommand AppendCommand => new AppendCommand(this);
    }
}
