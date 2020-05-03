using System.Windows.Controls;

namespace HorseAccounting.View
{
    /// <summary>
    /// Логика взаимодействия для AddHorse.xaml.
    /// </summary>
    public partial class AddHorse : Page
    {
        public AddHorse()
        {
            InitializeComponent();
        }

        private void SimpleAddBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            AddHorseSimple addHorseSimple = new AddHorseSimple();
            addHorseSimple.Show();
        }
    }
}
