using GalaSoft.MvvmLight;
using MySql.Data.MySqlClient;
using System;
using System.Collections.ObjectModel;

namespace HorseAccounting.Model
{
    public class Horse : ObservableObject
    {
        private int id;
        private int gpkNum;
        private string nickName;
        private int brand;
        private string bloodiness;
        private string color;
        private string birthDate;
        private string birthPlace;
        private string owner;

        private static MySqlConnection connection { get; set; }

        public int ID { get { return id; } set { Set<int>(() => this.ID, ref id, value); } }
        public int GpkNum { get { return gpkNum; } set { Set<int>(() => this.GpkNum, ref gpkNum, value); } }
        public string NickName { get { return nickName; } set { Set<string>(() => this.NickName, ref nickName, value); } }
        public int Brand { get { return brand; } set { Set<int>(() => this.Brand, ref brand, value); } }
        public string Bloodiness { get { return bloodiness; } set { Set<string>(() => this.Bloodiness, ref bloodiness, value); } }
        public string Color { get { return color; } set { Set<string>(() => this.Color, ref color, value); } }
        public string BirthDate { get { return birthDate; } set { Set<string>(() => this.BirthDate, ref birthDate, value); } }
        public string BirthPlace { get { return birthPlace; } set { Set<string>(() => this.BirthPlace, ref birthPlace, value); } }
        public string Owner { get { return owner; } set { Set<string>(() => this.Owner, ref owner, value); } }

        public static ObservableCollection<Horse> GetHorses()
        {
            ObservableCollection<Horse> horses = new ObservableCollection<Horse>();

            string connectionString = "SERVER=127.0.0.1;" + "DATABASE=horseaccounting;" + "UID=root;" + "PASSWORD=" + "" + ";";
            connection = new MySqlConnection(connectionString);
            string query = "SELECT * FROM лошадь";

            try
            {
                connection.Open();
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    horses.Add(
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
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return horses;
        }

        public static bool AddHorse(int gpk, string nick, int brand, string blodeness, string color, string dateBirth, string placeBirth, string owner)
        {
            string connectionString = "SERVER=127.0.0.1;" + "DATABASE=horseaccounting;" + "UID=root;" + "PASSWORD=" + "" + ";";
            connection = new MySqlConnection(connectionString);
            string query = "INSERT INTO `лошадь`(`№ по ГПК`, `Кличка`, `Тавро`, `Кровность`, `Масть`, `Пол`, `Дата рождения`, `Место рождения`, `Владелец`, `Мать`, `Отец`, `Чип`, `Выбытие`) " +
                "VALUES (" + gpk + ", '" + nick + "', '" + brand + "', '" + blodeness + "','" + color +
                "',2,'" + Convert.ToDateTime(dateBirth).ToString("yyyy-MM-dd") + "','" + placeBirth + "','" + owner + "',2,2,2,2)";

            try
            {
                connection.Open();
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command

                //Execute command
                cmd.ExecuteNonQuery();


                //close Connection
                connection.Close();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public void CleanHorseData()
        {
            GpkNum = 0;
            NickName = string.Empty;
            Brand = 0;
            Bloodiness = string.Empty;
            Color = string.Empty;
            BirthDate = string.Empty;
            BirthPlace = string.Empty;
            Owner = string.Empty;
        }
    }
}
