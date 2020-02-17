using GalaSoft.MvvmLight;
using MySql.Data.MySqlClient;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseAccounting.Model
{
    public class Progression : ObservableObject
    {
        #region Vars

        private int _id;
        private string _date;
        private string _destination;
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

        public string Destination
        {
            get { return _destination; }
            set { Set<string>(() => Destination, ref _destination, value); }
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

        public static ObservableCollection<Progression> GetSelectedProgression(int iD)
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

            string query = "SELECT * FROM `движение` Where `ID Лошади` = " + iD;

            ObservableCollection<Progression> selectedProgression = new ObservableCollection<Progression>();

            try
            {
                using (var sql = new MySqlConnection(Connection.ConnectionString))
                {
                    sql.Open();

                    MySqlCommand cmd = new MySqlCommand(query, sql);

                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    while (dataReader.Read())
                    {
                        selectedProgression.Add(
                            new Progression
                            {
                                ID = dataReader.GetInt32(0),
                                Date = dataReader.GetDateTime(1).ToShortDateString(),
                                Destination = dataReader.GetString(2),
                                Comment = dataReader.GetString(3),
                                HorseID = dataReader.GetInt32(4),
                            });
                    }

                    dataReader.Close();

                    sql.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                selectedProgression.Add(
                    new Progression
                    {
                        Date = "Данные не найдены",
                    });
            }

            return selectedProgression;
        }

        #endregion

        #region AddHorsePage

        public static bool AddProgression(string date, string destination, string comment, int horseID)
        {
            try
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

                using (var sql = new MySqlConnection(Connection.ConnectionString))
                {
                    sql.Open();

                    MySqlCommand cmd = sql.CreateCommand();
                    cmd.CommandText = "INSERT INTO `движение`(`Дата`, `Назначение`, `Комментарий`, `ID Лошади`) VALUES (@date, @destination, @comment, @horseID)";

                    cmd.Parameters.AddWithValue("@date", Convert.ToDateTime(date).ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@destination", destination);
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

        public void CleanProgressionData()
        {
            Date = string.Empty;
            Destination = string.Empty;
            Comment = string.Empty;
        }

        #endregion
    }
}
