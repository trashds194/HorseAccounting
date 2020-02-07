using GalaSoft.MvvmLight;
using MySql.Data.MySqlClient;
using Renci.SshNet;
using System;
using System.Collections.ObjectModel;

namespace HorseAccounting.Model
{
    public class Horse : ObservableObject
    {
        #region Vars

        private int _id;
        private int _gpkNum;
        private string _nickName;
        private int _brand;
        private string _bloodiness;
        private string _color;
        private string _gender;
        private string _birthDate;
        private string _birthPlace;
        private string _owner;
        private int _motherID;
        private int _fatherID;

        #endregion

        #region Definitions

        public int ID
        {
            get { return _id; }
            set { Set<int>(() => ID, ref _id, value); }
        }

        public int GpkNum
        {
            get { return _gpkNum; }
            set { Set<int>(() => GpkNum, ref _gpkNum, value); }
        }

        public string NickName
        {
            get { return _nickName; }
            set { Set<string>(() => NickName, ref _nickName, value); }
        }

        public int Brand
        {
            get { return _brand; }
            set { Set<int>(() => Brand, ref _brand, value); }
        }

        public string Bloodiness
        {
            get { return _bloodiness; }
            set { Set<string>(() => Bloodiness, ref _bloodiness, value); }
        }

        public string Color
        {
            get { return _color; }
            set { Set<string>(() => Color, ref _color, value); }
        }

        public string Gender
        {
            get { return _gender; }
            set { Set<string>(() => Gender, ref _gender, value); }
        }

        public string BirthDate
        {
            get { return _birthDate; }
            set { Set<string>(() => BirthDate, ref _birthDate, value); }
        }

        public string BirthPlace
        {
            get { return _birthPlace; }
            set { Set<string>(() => BirthPlace, ref _birthPlace, value); }
        }

        public string Owner
        {
            get { return _owner; }
            set { Set<string>(() => Owner, ref _owner, value); }
        }

        public int MotherID
        {
            get { return _motherID; }
            set { Set<int>(() => MotherID, ref _motherID, value); }
        }

        public int FatherID
        {
            get { return _fatherID; }
            set { Set<int>(() => FatherID, ref _fatherID, value); }
        }

        private static readonly SshClient SshConnection = new SshClient("hostru06.fornex.host", 20022, "t60064", "HR4M%rV~S8.pB$gc");
        private static readonly MySqlConnectionStringBuilder Connection = new MySqlConnectionStringBuilder();

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

            Connection.Server = "127.0.0.1";
            Connection.Port = 3306;

            Connection.UserID = "t60064_dbuser";
            Connection.Password = "HR4M%rV~S8.pB$gc";
            Connection.Database = "t60064_db";
            Connection.CharacterSet = "utf8";

            string query = "SELECT * FROM `лошадь`";

            ObservableCollection<Horse> horses = new ObservableCollection<Horse>();

            try
            {
                using (var sql = new MySqlConnection(Connection.ConnectionString))
                {
                    sql.Open();

                    MySqlCommand cmd = new MySqlCommand(query, sql);

                    MySqlDataReader dataReader = cmd.ExecuteReader();

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

                    dataReader.Close();

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
                        Bloodiness = "Обратитесь к разработчику приложения!",
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

            Connection.Server = "127.0.0.1";
            Connection.Port = 3306;

            Connection.UserID = "t60064_dbuser";
            Connection.Password = "HR4M%rV~S8.pB$gc";
            Connection.Database = "t60064_db";
            Connection.CharacterSet = "utf8";

            string query = "SELECT * FROM `лошадь` where Пол = 'Кобыла'";

            ObservableCollection<Horse> motherHorses = new ObservableCollection<Horse>();

            try
            {
                using (var sql = new MySqlConnection(Connection.ConnectionString))
                {
                    sql.Open();

                    MySqlCommand cmd = new MySqlCommand(query, sql);

                    MySqlDataReader dataReader = cmd.ExecuteReader();

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

                    dataReader.Close();

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
                        Bloodiness = "Обратитесь к разработчику приложения!",
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

            Connection.Server = "127.0.0.1";
            Connection.Port = 3306;

            Connection.UserID = "t60064_dbuser";
            Connection.Password = "HR4M%rV~S8.pB$gc";
            Connection.Database = "t60064_db";
            Connection.CharacterSet = "utf8";

            string query = "SELECT * FROM `лошадь` where Пол = 'Жеребец'";

            ObservableCollection<Horse> fatherHorses = new ObservableCollection<Horse>();

            try
            {
                using (var sql = new MySqlConnection(Connection.ConnectionString))
                {
                    sql.Open();

                    MySqlCommand cmd = new MySqlCommand(query, sql);

                    MySqlDataReader dataReader = cmd.ExecuteReader();

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

                    dataReader.Close();

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
                        Bloodiness = "Обратитесь к разработчику приложения!",
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

                Connection.Server = "127.0.0.1";
                Connection.Port = 3306;

                Connection.UserID = "t60064_dbuser";
                Connection.Password = "HR4M%rV~S8.pB$gc";
                Connection.Database = "t60064_db";
                Connection.CharacterSet = "utf8";

                string query = "INSERT INTO `лошадь`(`№ по ГПК`, `Кличка`, `Тавро`, `Кровность`, `Масть`, `Пол`, `Дата рождения`, `Место рождения`, `Владелец`, `Мать`, `Отец`, `Чип`, `Выбытие`) " +
                    "VALUES (" + gpk + ", '" + nick + "', '" + brand + "', '" + blodeness + "','" + color +
                    "', '" + gend + "', '" + Convert.ToDateTime(dateBirth).ToString("yyyy-MM-dd") + "','" + placeBirth + "','" + owner + "', " + motherID + ", " + fatherID + ",2,2)";

                using (var sql = new MySqlConnection(Connection.ConnectionString))
                {
                    sql.Open();

                    MySqlCommand cmd = new MySqlCommand(query, sql);

                    cmd.ExecuteNonQuery();

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

        public static ObservableCollection<Horse> GetSelectedHorse(int iD)
        {
            if (!SshConnection.IsConnected)
            {
                SshConnection.Connect();
                ForwardedPortLocal port = new ForwardedPortLocal("127.0.0.1", 3306, "127.0.0.1", 3306);
                SshConnection.AddForwardedPort(port);

                port.Start();
            }

            Connection.Server = "127.0.0.1";
            Connection.Port = 3306;

            Connection.UserID = "t60064_dbuser";
            Connection.Password = "HR4M%rV~S8.pB$gc";
            Connection.Database = "t60064_db";
            Connection.CharacterSet = "utf8";

            string query = "SELECT * FROM `лошадь` Where ID = " + iD;

            ObservableCollection<Horse> selectedHorse = new ObservableCollection<Horse>();

            try
            {
                using (var sql = new MySqlConnection(Connection.ConnectionString))
                {
                    sql.Open();

                    MySqlCommand cmd = new MySqlCommand(query, sql);

                    MySqlDataReader dataReader = cmd.ExecuteReader();

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

                    dataReader.Close();

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
                        Bloodiness = "Обратитесь к разработчику приложения!",
                    });
            }

            return selectedHorse;
        }

        #endregion
    }
}
