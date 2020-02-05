using GalaSoft.MvvmLight;
using MySql.Data.MySqlClient;
using Renci.SshNet;
using System;
using System.Collections.ObjectModel;
using System.Text;

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

        private static SshClient SshConnection = new SshClient("hostru06.fornex.host", 20022, "t60064", "HR4M%rV~S8.pB$gc");
        private static MySqlConnectionStringBuilder connection = new MySqlConnectionStringBuilder();

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
        public int MotherID { get { return motherID; } set { Set<int>(() => this.MotherID, ref motherID, value); } }
        public int FatherID { get { return fatherID; } set { Set<int>(() => this.FatherID, ref fatherID, value); } }

        #endregion

        #region HorsesListPage

        public static ObservableCollection<Horse> GetHorses()
        {

            if (!SshConnection.IsConnected)
            {
                SshConnection.Connect();
                ForwardedPortLocal port = new ForwardedPortLocal("127.0.0.1", 3306, "127.0.0.1", 3306);
                SshConnection.AddForwardedPort(port);

                port.Start();
            }

            connection.Server = "127.0.0.1";
            connection.Port = 3306;

            connection.UserID = "t60064_dbuser";
            connection.Password = "HR4M%rV~S8.pB$gc";
            connection.Database = "t60064_db";
            connection.CharacterSet = "utf8";

            string query = "SELECT * FROM `лошадь`";

            ObservableCollection<Horse> horses = new ObservableCollection<Horse>();

            try
            {
                using (var sql = new MySqlConnection(connection.ConnectionString))
                {
                    sql.Open();
                    //Create Command
                    MySqlCommand cmd = new MySqlCommand(query, sql);
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
                    sql.Close();
                }
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
            if (!SshConnection.IsConnected)
            {
                SshConnection.Connect();
                ForwardedPortLocal port = new ForwardedPortLocal("127.0.0.1", 3306, "127.0.0.1", 3306);
                SshConnection.AddForwardedPort(port);

                port.Start();
            }

            connection.Server = "127.0.0.1";
            connection.Port = 3306;

            connection.UserID = "t60064_dbuser";
            connection.Password = "HR4M%rV~S8.pB$gc";
            connection.Database = "t60064_db";
            connection.CharacterSet = "utf8";

            string query = "SELECT * FROM `лошадь` where Пол = 'Кобыла'";

            Console.WriteLine(query);

            ObservableCollection<Horse> motherHorses = new ObservableCollection<Horse>();

            try
            {
                using (var sql = new MySqlConnection(connection.ConnectionString))
                {
                    sql.Open();
                    //Create Command

                    MySqlCommand cmd = new MySqlCommand(query, sql);

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
                    sql.Close();
                }
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
            if (!SshConnection.IsConnected)
            {
                SshConnection.Connect();
                ForwardedPortLocal port = new ForwardedPortLocal("127.0.0.1", 3306, "127.0.0.1", 3306);
                SshConnection.AddForwardedPort(port);

                port.Start();
            }

            connection.Server = "127.0.0.1";
            connection.Port = 3306;

            connection.UserID = "t60064_dbuser";
            connection.Password = "HR4M%rV~S8.pB$gc";
            connection.Database = "t60064_db";
            connection.CharacterSet = "utf8";

            string query = "SELECT * FROM `лошадь` where Пол = 'Жеребец'";

            ObservableCollection<Horse> fatherHorses = new ObservableCollection<Horse>();

            try
            {
                using (var sql = new MySqlConnection(connection.ConnectionString))
                {
                    sql.Open();
                    //Create Command
                    MySqlCommand cmd = new MySqlCommand(query, sql);
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
                    sql.Close();
                }
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
                if (!SshConnection.IsConnected)
                {
                    SshConnection.Connect();
                    ForwardedPortLocal port = new ForwardedPortLocal("127.0.0.1", 3306, "127.0.0.1", 3306);
                    SshConnection.AddForwardedPort(port);

                    port.Start();
                }

                connection.Server = "127.0.0.1";
                connection.Port = 3306;

                connection.UserID = "t60064_dbuser";
                connection.Password = "HR4M%rV~S8.pB$gc";
                connection.Database = "t60064_db";
                connection.CharacterSet = "utf8";

                string query = "INSERT INTO `лошадь`(`№ по ГПК`, `Кличка`, `Тавро`, `Кровность`, `Масть`, `Пол`, `Дата рождения`, `Место рождения`, `Владелец`, `Мать`, `Отец`, `Чип`, `Выбытие`) " +
                    "VALUES (" + gpk + ", '" + nick + "', '" + brand + "', '" + blodeness + "','" + color +
                    "', '" + gend + "', '" + Convert.ToDateTime(dateBirth).ToString("yyyy-MM-dd") + "','" + placeBirth + "','" + owner + "', " + motherID + ", " + fatherID + ",2,2)";

                using (var sql = new MySqlConnection(connection.ConnectionString))
                {
                    sql.Open();
                    //Create Command
                    MySqlCommand cmd = new MySqlCommand(query, sql);
                    //Create a data reader and Execute the command

                    //Execute command
                    cmd.ExecuteNonQuery();


                    //close Connection
                    sql.Close();

                    return true;
                }
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
            if (!SshConnection.IsConnected)
            {
                SshConnection.Connect();
                ForwardedPortLocal port = new ForwardedPortLocal("127.0.0.1", 3306, "127.0.0.1", 3306);
                SshConnection.AddForwardedPort(port);

                port.Start();
            }

            connection.Server = "127.0.0.1";
            connection.Port = 3306;

            connection.UserID = "t60064_dbuser";
            connection.Password = "HR4M%rV~S8.pB$gc";
            connection.Database = "t60064_db";
            connection.CharacterSet = "utf8";

            string query = "SELECT * FROM `лошадь` Where ID = " + ID;

            ObservableCollection<Horse> selectedHorse = new ObservableCollection<Horse>();

            try
            {
                using (var sql = new MySqlConnection(connection.ConnectionString))
                {
                    sql.Open();
                    //Create Command
                    MySqlCommand cmd = new MySqlCommand(query, sql);
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
                    sql.Close();
                }
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
