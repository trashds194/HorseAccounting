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
        private int _origin;
        private int _typicality;
        private int _measurements;
        private int _exterior;
        private int _workingCapacity;
        private int _offspringQuality;
        private string _theClass;
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

        public int Origin
        {
            get { return _origin; }
            set { Set<int>(() => Origin, ref _origin, value); }
        }

        public int Typicality
        {
            get { return _typicality; }
            set { Set<int>(() => Typicality, ref _typicality, value); }
        }

        public int Measurements
        {
            get { return _measurements; }
            set { Set<int>(() => Measurements, ref _measurements, value); }
        }

        public int Exterior
        {
            get { return _exterior; }
            set { Set<int>(() => Exterior, ref _exterior, value); }
        }

        public int WorkingCapacity
        {
            get { return _workingCapacity; }
            set { Set<int>(() => WorkingCapacity, ref _workingCapacity, value); }
        }

        public int OffspringQuality
        {
            get { return _offspringQuality; }
            set { Set<int>(() => OffspringQuality, ref _offspringQuality, value); }
        }

        public string TheClass
        {
            get { return _theClass; }
            set { Set<string>(() => TheClass, ref _theClass, value); }
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
                                Origin = dataReader.GetInt32(2),
                                Typicality = dataReader.GetInt32(3),
                                Measurements = dataReader.GetInt32(4),
                                Exterior = dataReader.GetInt32(5),
                                WorkingCapacity = dataReader.GetInt32(6),
                                OffspringQuality = dataReader.GetInt32(7),
                                TheClass = dataReader.GetString(8),
                                HorseID = dataReader.GetInt32(9),
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

            #endregion
        }
    }
}