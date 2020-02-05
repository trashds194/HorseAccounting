using MySql.Data.MySqlClient;

namespace HorseAccounting
{
    public static class DbConnection
    {
        private static MySqlConnectionStringBuilder connection;

        public static MySqlConnectionStringBuilder Connection
        {
            get
            {
                connection.Server = "127.0.0.1";
                connection.Port = 3306;

                connection.UserID = "t60064_dbuser";
                connection.Password = "HR4M%rV~S8.pB$gc";
                connection.Database = "t60064_db";
                return connection;
            }
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
