using Calculator.Core;
using Calculator.Ui.Wpf.Model;
using Calculator.Ui.Wpf.ViewModel;
using System;
using System.Windows.Input;

namespace Calculator.Ui.Wpf.Commands
{
    public class InterpetCommand : ICommand
    {
        private InterpteterViewModel _vm;

        public event EventHandler CanExecuteChanged;

        public InterpetCommand(InterpteterViewModel vm)
        {
            _vm = vm;
        }

        public bool CanExecute(object parameter)
        {
            return !string.IsNullOrEmpty(_vm.Expression);
        }

        public void Execute(object parameter)
        {
            try
            {
                var value = new Interpreter(_vm.Expression).Interpret();
                _vm.HistoryItems.AddAndNotify(new HistoryItem
                {
                    Expression = _vm.Expression,
                    Value = value.ToString()
                });
                _vm.Expression = string.Empty;
            }
            catch (Exception ex)
            {
                _vm.HistoryItems.AddAndNotify(new HistoryItem
                {
                    Expression = _vm.Expression,
                    Value = ex.Message
                });
            }
            _vm.SymbolItems.Update();
        }
    }
}
