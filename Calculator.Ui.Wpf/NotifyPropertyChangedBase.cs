using System;
using System.ComponentModel;

namespace Calculator.Ui.Wpf
{
    public abstract class NotifyPropertyChangedBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void ChangeProperty(Action action, string propertyName)
        {
            action?.Invoke();
            OnPropertyChanged(propertyName);
        }
    }
}
