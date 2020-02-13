using GalaSoft.MvvmLight;
using MySql.Data.MySqlClient;
using Renci.SshNet;
using System;
using System.Collections.ObjectModel;

namespace HorseAccounting.Model
{
    public class Scoring : ObservableObject
    {
        #region Vars

        private int _id;
        private string _date;
        private string _boniter;
        private int _origin;
        private int _typicality;
        private int _measurements;
        private int _exterior;
        private int _workingCapacity;
        private int _offspringQuality;
        private string _theClass;
        private string _comment;
        private int _horseID;

        #endregion

        #region Definitions

        public int ID
        {
            get { return _id; }
            set { Set<int>(() => ID, ref _id, value); }
        }

        public string Date
        {
            get { return _date; }
            set { Set<string>(() => Date, ref _date, value); }
        }

        public string Boniter
        {
            get { return _boniter; }
            set { Set<string>(() => Boniter, ref _boniter, value); }
        }

        public int Origin
        {
            get { return _origin; }
            set
            {
                if (value <= 10)
                    Set<int>(() => Origin, ref _origin, value);
            }
        }

        public int Typicality
        {
            get { return _typicality; }
            set
            {
                if (value <= 10)
                    Set<int>(() => Typicality, ref _typicality, value);
            }
        }

        public int Measurements
        {
            get { return _measurements; }
            set
            {
                if (value <= 10)
                    Set<int>(() => Measurements, ref _measurements, value);
            }
        }

        public int Exterior
        {
            get { return _exterior; }
            set
            {
                if (value <= 10)
                    Set<int>(() => Exterior, ref _exterior, value);
            }
        }

        public int WorkingCapacity
        {
            get { return _workingCapacity; }
            set
            {
                if (value <= 10)
                    Set<int>(() => WorkingCapacity, ref _workingCapacity, value);
            }
        }

        public int OffspringQuality
        {
            get { return _offspringQuality; }
            set
            {
                if (value <= 10)
                    Set<int>(() => OffspringQuality, ref _offspringQuality, value);
            }
        }

        public string TheClass
        {
            get { return _theClass; }
            set { Set<string>(() => TheClass, ref _theClass, value); }
        }

        public string Comment
        {
            get { return _comment; }
            set { Set<string>(() => Comment, ref _comment, value); }
        }

        public int HorseID
        {
            get { return _horseID; }
            set { Set<int>(() => HorseID, ref _horseID, value); }
        }

        private static readonly SshClient SshConnection = new SshClient("hostru06.fornex.host", 20022, "t60064", "HR4M%rV~S8.pB$gc");
        private static readonly MySqlConnectionStringBuilder Connection = new MySqlConnectionStringBuilder();

        #endregion

        #region ShowHorsePage

        public static ObservableCollection<Scoring> GetSelectedScoring(int iD)
        {
            if (!SshConnection.IsConnected)
            {
                SshConnection.Connect();
                ForwardedPortLocal port = new ForwardedPortLocal("127.0.0.1", 3306, "127.0.0.1", 3306);
                SshConnection.AddForwardedPort(port);

                //port.Start();
            }

            Connection.Server = "127.0.0.1";
            Connection.Port = 3306;

            Connection.UserID = "t60064_dbuser";
            Connection.Password = "HR4M%rV~S8.pB$gc";
            Connection.Database = "t60064_db";
            Connection.CharacterSet = "utf8";

            string query = "SELECT * FROM `бонитировка` Where `ID Лошади` = " + iD;

            ObservableCollection<Scoring> selectedScoring = new ObservableCollection<Scoring>();

            try
            {
                using (var sql = new MySqlConnection(Connection.ConnectionString))
                {
                    sql.Open();

                    MySqlCommand cmd = new MySqlCommand(query, sql);

                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    while (dataReader.Read())
                    {
                        selectedScoring.Add(
                            new Scoring
                            {
                                ID = dataReader.GetInt32(0),
                                Date = dataReader.GetDateTime(1).ToShortDateString(),
                                Boniter = dataReader.GetString(2),
                                Origin = dataReader.GetInt32(3),
                                Typicality = dataReader.GetInt32(4),
                                Measurements = dataReader.GetInt32(5),
                                Exterior = dataReader.GetInt32(6),
                                WorkingCapacity = dataReader.GetInt32(7),
                                OffspringQuality = dataReader.GetInt32(8),
                                TheClass = dataReader.GetString(9),
                                Comment = dataReader.GetString(10),
                                HorseID = dataReader.GetInt32(11),
                            });
                    }

                    dataReader.Close();

                    sql.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                selectedScoring.Add(
                    new Scoring
                    {
                        Date = "Данные не найдены",
                    });
            }

            return selectedScoring;
        }

        #endregion

        #region AddScoringPage

        public static bool AddScoring(string date, string boniter, int origin, int typicality, int measure, int exterior, int workingCapacity, int offspringQuality, string theClass, string comment, int horseID)
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

                using (var sql = new MySqlConnection(Connection.ConnectionString))
                {
                    sql.Open();

                    MySqlCommand cmd = sql.CreateCommand();
                    cmd.CommandText = "INSERT INTO `бонитировка`(`Дата бонитировки`, `Бонитер`, `Происхождение`, `Типичность`, `Промеры`, `Экстерьер`, `Работоспособность`, `Качество потомства`, `Класс`, `Комментарий`, `ID Лошади`) " +
                    "VALUES (@date, @boniter, @origin, @typicality, @measure, @exterior, @workingCapacity, @offspringQuality, @theClass, @comment, @horseID)";

                    cmd.Parameters.AddWithValue("@date", Convert.ToDateTime(date).ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@boniter", boniter);
                    cmd.Parameters.AddWithValue("@origin", origin);
                    cmd.Parameters.AddWithValue("@typicality", typicality);
                    cmd.Parameters.AddWithValue("@measure", measure);
                    cmd.Parameters.AddWithValue("@exterior", exterior);
                    cmd.Parameters.AddWithValue("@workingCapacity", workingCapacity);
                    cmd.Parameters.AddWithValue("@offspringQuality", offspringQuality);
                    cmd.Parameters.AddWithValue("@theClass", theClass);
                    cmd.Parameters.AddWithValue("@comment", comment);
                    cmd.Parameters.AddWithValue("@horseID", horseID);

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

        public void CleanScoringData()
        {
            Date = string.Empty;
            Boniter = string.Empty;
            Origin = 0;
            Typicality = 0;
            Measurements = 0;
            Exterior = 0;
            WorkingCapacity = 0;
            OffspringQuality = 0;
            TheClass = string.Empty;
            Comment = string.Empty;
        }
        #endregion
    }
}