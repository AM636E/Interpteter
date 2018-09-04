using Calculator.Ui.Wpf.ViewModel;
using System;
using System.Windows.Input;

namespace Calculator.Ui.Wpf.Commands
{
    internal class AppendCommand : ICommand
    {
        private InterpteterViewModel interpteterViewModel;

        public AppendCommand(InterpteterViewModel interpteterViewModel)
        {
            this.interpteterViewModel = interpteterViewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return !string.IsNullOrEmpty(parameter?.ToString());
        }

        public void Execute(object parameter)
        {
            interpteterViewModel.Expression += parameter;
        }
    }
}