using MySql.Data.MySqlClient;
using System.Windows;

namespace HorseAccounting
{
    class DbConnection
    {
        public static MySqlConnection connection { get; set; }
        public string Server { get; set; }
        public string Database { get; set; }
        public string Uid { get; set; }
        public string Password { get; set; }

        public DbConnection()
        {
            Initialize();
        }

        private void Initialize()
        {
            Server = "127.0.0.1:3306";
            Database = "horseaccounting";
            Uid = "root";
            Password = "";
            string connectionString = "SERVER=" + Server + ";" + "DATABASE=" + Database + ";" + "UID=" + Uid + ";" + "PASSWORD=" + Password + ";";
            connection = new MySqlConnection(connectionString);
        }

    //    public static bool OpenConnection()
    //    {
    //        try
    //        {
    //            connection.Open();
    //            return true;
    //        }
    //        catch (MySqlException ex)
    //        {
    //            switch (ex.Number)
    //            {
    //                case 0:
    //                    MessageBox.Show("Cannot connect to server.  Contact administrator");
    //                    break;

    //                case 1045:
    //                    MessageBox.Show("Invalid username/password, please try again");
    //                    break;
    //            }
    //            return false;
    //        }
    //    }

    //    public static bool CloseConnection()
    //    {
    //        try
    //        {
    //            connection.Close();
    //            return true;
    //        }
    //        catch (MySqlException ex)
    //        {
    //            MessageBox.Show(ex.Message);
    //            return false;
    //        }
    //    }
    }
}
