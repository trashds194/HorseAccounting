using GalaSoft.MvvmLight;
using MySql.Data.MySqlClient;
using System;
using System.Collections.ObjectModel;

namespace HorseAccounting.Model
{
    public class Horse : ObservableObject
    {
        #region Vars 

        private int id;
        private int gpkNum;
        private string nickName;
        private int brand;
        private string bloodiness;
        private string color;
        private string gender;
        private string birthDate;
        private string birthPlace;
        private string owner;
        private int motherID;
        private int fatherID;

        #endregion

        #region Definitions

        private static MySqlConnection connection { get; set; }

        public int ID { get { return id; } set { Set<int>(() => this.ID, ref id, value); } }
        public int GpkNum { get { return gpkNum; } set { Set<int>(() => this.GpkNum, ref gpkNum, value); } }
        public string NickName { get { return nickName; } set { Set<string>(() => this.NickName, ref nickName, value); } }
        public int Brand { get { return brand; } set { Set<int>(() => this.Brand, ref brand, value); } }
        public string Bloodiness { get { return bloodiness; } set { Set<string>(() => this.Bloodiness, ref bloodiness, value); } }
        public string Color { get { return color; } set { Set<string>(() => this.Color, ref color, value); } }
        public string Gender { get { return gender; } set { Set<string>(() => this.Gender, ref gender, value); } }
        public string BirthDate { get { return birthDate; } set { Set<string>(() => this.BirthDate, ref birthDate, value); } }
        public string BirthPlace { get { return birthPlace; } set { Set<string>(() => this.BirthPlace, ref birthPlace, value); } }
        public string Owner { get { return owner; } set { Set<string>(() => this.Owner, ref owner, value); } }
        public int MotherID{ get { return motherID; } set { Set<int>(() => this.MotherID, ref motherID, value); } }
        public int FatherID { get { return fatherID; } set { Set<int>(() => this.FatherID, ref fatherID, value); } }

        #endregion

        #region HorsesListPage

        public static ObservableCollection<Horse> GetHorses()
        {
            ObservableCollection<Horse> horses = new ObservableCollection<Horse>();

            string connectionString = "SERVER = 127.0.0.1; " + "DATABASE = horseaccounting; " + "UID = root; " + "PASSWORD = " + "" + "";
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
                            ID = dataReader.GetInt32(0),
                            GpkNum = dataReader.GetInt32(1),
                            NickName = dataReader.GetString(2),
                            Brand = dataReader.GetInt32(3),
                            Bloodiness = dataReader.GetString(4),
                            Color = dataReader.GetString(5),
                            Gender = dataReader.GetString(6),
                            BirthDate = dataReader.GetDateTime(7).ToShortDateString(),
                            BirthPlace = dataReader.GetString(8),
                            Owner = dataReader.GetString(9),
                            MotherID = dataReader.GetInt32(10),
                            FatherID = dataReader.GetInt32(11),
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
                horses.Add(
                    new Horse
                    {
                        NickName = "База данных не найдена!",
                        Bloodiness = "Обратитесь к разработчику приложения!"
                    });
            }

            return horses;
        }

        #endregion

        #region AddHorsePage

        public static ObservableCollection<Horse> GetMotherHorse()
        {
            ObservableCollection<Horse> motherHorses = new ObservableCollection<Horse>();

            string connectionString = "SERVER=127.0.0.1;" + "DATABASE=horseaccounting;" + "UID=root;" + "PASSWORD=" + "" + ";";
            connection = new MySqlConnection(connectionString);
            string query = "SELECT * FROM лошадь where Пол = 'Кобыла'";

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
                    motherHorses.Add(
                        new Horse
                        {
                            ID = dataReader.GetInt32(0),
                            GpkNum = dataReader.GetInt32(1),
                            NickName = dataReader.GetString(2),
                            Brand = dataReader.GetInt32(3),
                            Bloodiness = dataReader.GetString(4),
                            Color = dataReader.GetString(5),
                            Gender = dataReader.GetString(6),
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
                motherHorses.Add(
                    new Horse
                    {
                        NickName = "База данных не найдена!",
                        Bloodiness = "Обратитесь к разработчику приложения!"
                    });
            }

            return motherHorses;
        }

        public static ObservableCollection<Horse> GetFatherHorse()
        {
            ObservableCollection<Horse> fatherHorses = new ObservableCollection<Horse>();

            string connectionString = "SERVER=127.0.0.1;" + "DATABASE=horseaccounting;" + "UID=root;" + "PASSWORD=" + "" + ";";
            connection = new MySqlConnection(connectionString);
            string query = "SELECT * FROM лошадь where Пол = 'Жеребец'";

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
                    fatherHorses.Add(
                        new Horse
                        {
                            ID = dataReader.GetInt32(0),
                            GpkNum = dataReader.GetInt32(1),
                            NickName = dataReader.GetString(2),
                            Brand = dataReader.GetInt32(3),
                            Bloodiness = dataReader.GetString(4),
                            Color = dataReader.GetString(5),
                            Gender = dataReader.GetString(6),
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
                fatherHorses.Add(
                    new Horse
                    {
                        NickName = "База данных не найдена!",
                        Bloodiness = "Обратитесь к разработчику приложения!"
                    });
            }

            return fatherHorses;
        }

        public static bool AddHorse(int gpk, string nick, int brand, string blodeness, string color, string gend, string dateBirth, string placeBirth, string owner, int motherID, int fatherID)
        {
            try
            {
                string connectionString = "SERVER=127.0.0.1;" + "DATABASE=horseaccounting;" + "UID=root;" + "PASSWORD=" + "" + ";";
                connection = new MySqlConnection(connectionString);
                string query = "INSERT INTO `лошадь`(`№ по ГПК`, `Кличка`, `Тавро`, `Кровность`, `Масть`, `Пол`, `Дата рождения`, `Место рождения`, `Владелец`, `Мать`, `Отец`, `Чип`, `Выбытие`) " +
                    "VALUES (" + gpk + ", '" + nick + "', '" + brand + "', '" + blodeness + "','" + color +
                    "', '" + gend + "', '" + Convert.ToDateTime(dateBirth).ToString("yyyy-MM-dd") + "','" + placeBirth + "','" + owner + "', " + motherID + ", " + fatherID + ",2,2)";

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
        
        #endregion

        #region ShowHorsePage

        public static ObservableCollection<Horse> GetSelectedHorse(int ID)
        {
            ObservableCollection<Horse> selectedHorse = new ObservableCollection<Horse>();

            string connectionString = "SERVER=127.0.0.1;" + "DATABASE=horseaccounting;" + "UID=root;" + "PASSWORD=" + "" + ";";
            connection = new MySqlConnection(connectionString);
            string query = "SELECT * FROM лошадь Where ID = " + ID;

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
                    selectedHorse.Add(
                        new Horse
                        {
                            ID = dataReader.GetInt32(0),
                            GpkNum = dataReader.GetInt32(1),
                            NickName = dataReader.GetString(2),
                            Brand = dataReader.GetInt32(3),
                            Bloodiness = dataReader.GetString(4),
                            Color = dataReader.GetString(5),
                            Gender = dataReader.GetString(6),
                            BirthDate = dataReader.GetDateTime(7).ToShortDateString(),
                            BirthPlace = dataReader.GetString(8),
                            Owner = dataReader.GetString(9),
                            MotherID = dataReader.GetInt32(10),
                            FatherID = dataReader.GetInt32(11),
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
                selectedHorse.Add(
                    new Horse
                    {
                        NickName = "База данных не найдена!",
                        Bloodiness = "Обратитесь к разработчику приложения!"
                    });
            }

            return selectedHorse;
        }

        #endregion        
    }
}
