using GalaSoft.MvvmLight.Command;
using HorseAccounting.Infra;
using HorseAccounting.Model;
using MySql.Data.MySqlClient;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace HorseAccounting.ViewModel
{
    public class HorsesListViewModel : NavigateViewModel
    {
        private Horse horse;
        private MySqlConnection connection1 { get; set; }

        public ObservableCollection<Horse> Horses { get; set; }
        public Horse Horse { get { return horse; } set { horse = value; } }

        public HorsesListViewModel()
        {
            Title = "Page1";
            GetData();
        }

        private ICommand _addHorse;

        public ICommand AddHorse
        {
            get
            {
                if (_addHorse == null)
                {
                    _addHorse = new RelayCommand(() =>
                    {
                        Navigate("View/AddHorse.xaml");
                    });
                }
                return _addHorse;
            }
            set { _addHorse = value; }
        }

        private void GetData()
        {
            Horses = new ObservableCollection<Horse>();
            string connectionString = "SERVER=127.0.0.1;" + "DATABASE=horseaccounting;" + "UID=root;" + "PASSWORD=" + "" + ";";
            connection1 = new MySqlConnection(connectionString);
            string query = "SELECT * FROM лошадь";

            connection1.Open();
            //Create Command
            MySqlCommand cmd = new MySqlCommand(query, connection1);
            //Create a data reader and Execute the command
            MySqlDataReader dataReader = cmd.ExecuteReader();

            //Read the data and store them in the list
            while (dataReader.Read())
            {
                Horses.Add(
                    new Horse
                    {
                        GpkNum = dataReader.GetInt32(1),
                        NickName = dataReader.GetString(2),
                        Brand = dataReader.GetInt32(3),
                        Bloodiness = dataReader.GetString(4),
                        Color = dataReader.GetString(5),
                        BirthDate = dataReader.GetDateTime(7).ToShortDateString(),
                        BirthPlace = dataReader.GetString(8),
                        Owner = dataReader.GetString(9),
                    });
            }


            //close Data Reader
            dataReader.Close();

            //close Connection
            connection1.Close();
        }
    }
}
