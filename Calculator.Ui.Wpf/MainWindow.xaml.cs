using Calculator.Ui.Wpf.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace Calculator.Ui.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            var tb = (TextBox)sender;
            //if (tb.Text.Length > 0)
            //{
            //    tb.SelectionStart = tb.Text.Length;
            //    tb.SelectionLength = 0;
            //}
        }
    }
}
