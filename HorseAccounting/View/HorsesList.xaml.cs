using System;
using System.Windows.Controls;

namespace HorseAccounting.View
{
    /// <summary>
    /// Логика взаимодействия для HorsesList.xaml.
    /// </summary>
    public partial class HorsesList : Page
    {
        public HorsesList()
        {
            InitializeComponent();
        }

        private void TextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = !(Char.IsDigit(e.Text, 0));
        }
    }
}
