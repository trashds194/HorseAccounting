using GalaSoft.MvvmLight.Messaging;
using MySql.Data.MySqlClient;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseAccounting.Infra
{
    public static class DbConnection
    {
        #region Vars

        private static readonly SshClient sshConnection = new SshClient(Properties.Resources.HostName, 20022, Properties.Resources.HostLogin, Properties.Resources.HostPassword);
        private static readonly MySqlConnectionStringBuilder connection = new MySqlConnectionStringBuilder();

        #endregion

        #region Definitions

        public static SshClient SshConnection => sshConnection;

        public static MySqlConnectionStringBuilder Connection => connection;

        #endregion

        public static void CreateConnection()
        {
            try
            {
                if (!SshConnection.IsConnected)
                {
                    SshConnection.Connect();
                    ForwardedPortLocal port = new ForwardedPortLocal(Properties.Resources.LocalIP, 3306, Properties.Resources.LocalIP, 3306);
                    SshConnection.AddForwardedPort(port);

                    if (!port.IsStarted)
                    {
                        port.Start();
                    }
                }

                Connection.Server = Properties.Resources.LocalIP;
                Connection.Port = 3306;

                Connection.UserID = Properties.Resources.DbUser;
                Connection.Password = Properties.Resources.DbPassword;
                Connection.Database = Properties.Resources.DbName;
                Connection.CharacterSet = Properties.Resources.DbCharSet;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Невозможно подключиться к серверу! " +
                    "Проверьте соединение с интернетом или обратитесь к разработчику!"));
            }
        }
    }
}
