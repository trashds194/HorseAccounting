using GalaSoft.MvvmLight;
using HorseAccounting.Infra;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;

namespace HorseAccounting.Model
{
    public class Horse : ObservableObject
    {
        #region Vars

        private int _id;
        private string _gpkNum;
        private string _nickName;
        private string _brand;
        private string _bloodiness;
        private string _color;
        private string _gender;
        private string _birthDate;
        private string _birthPlace;
        private string _owner;
        private int _motherID;
        private int _fatherID;
        private string _fullName;
        private string _state;

        private static readonly HttpClient client = new HttpClient();

        #endregion

        #region Definitions

        public int ID
        {
            get { return _id; }
            set { Set<int>(() => ID, ref _id, value); }
        }

        public string GpkNum
        {
            get { return _gpkNum; }
            set { Set<string>(() => GpkNum, ref _gpkNum, value); }
        }

        public string NickName
        {
            get { return _nickName; }
            set { Set<string>(() => NickName, ref _nickName, value); }
        }

        public string Brand
        {
            get { return _brand; }
            set { Set<string>(() => Brand, ref _brand, value); }
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

        public string State
        {
            get { return _state; }
            set { Set<string>(() => State, ref _state, value); }
        }

        public string FullName
        {
            get { return _fullName; }
            set { Set<string>(() => FullName, ref _fullName, value); }
        }

        #endregion

        #region HorsesListPage

        public static async Task<ObservableCollection<Horse>> GetHorses()
        {
            string url = "http://1k-horse-base.loc/HorseAccountingApi/horse/horse.php?gethorse=all";

            string response = await client.GetStringAsync(url);

            ObservableCollection<Horse> horses = JsonConvert.DeserializeObject<ObservableCollection<Horse>>(response);

            return horses;
        }

        public static async Task<ObservableCollection<Horse>> GetActingHorses()
        {
            string url = "http://1k-horse-base.loc/HorseAccountingApi/horse/horse.php?gethorse=acting";

            string response = await client.GetStringAsync(url);

            ObservableCollection<Horse> horses = JsonConvert.DeserializeObject<ObservableCollection<Horse>>(response);

            return horses;
        }

        public static async Task<ObservableCollection<Horse>> GetRetiredHorses()
        {
            string url = "http://1k-horse-base.loc/HorseAccountingApi/horse/horse.php?gethorse=retired";

            string response = await client.GetStringAsync(url);

            ObservableCollection<Horse> horses = JsonConvert.DeserializeObject<ObservableCollection<Horse>>(response);

            return horses;
        }

        public static async Task<ObservableCollection<Horse>> SearchHorsesAsync(string searchQuery)
        {
            string url = "http://1k-horse-base.loc/HorseAccountingApi/horse/horse.php?search=" + searchQuery;

            string response = await client.GetStringAsync(url);

            ObservableCollection<Horse> searchedHorses = JsonConvert.DeserializeObject<ObservableCollection<Horse>>(response);

            return searchedHorses;
        }

        #endregion

        #region AddHorsePage

        public static async Task<ObservableCollection<Horse>> GetMotherHorseAsync()
        {
            string url = "http://1k-horse-base.loc/HorseAccountingApi/horse/horse.php?gethorse=mother";

            string response = await client.GetStringAsync(url);

            ObservableCollection<Horse> motherHorses = JsonConvert.DeserializeObject<ObservableCollection<Horse>>(response);

            return motherHorses;
        }

        public static async Task<ObservableCollection<Horse>> GetFatherHorseAsync()
        {
            string url = "http://1k-horse-base.loc/HorseAccountingApi/horse/horse.php?gethorse=father";

            string response = await client.GetStringAsync(url);

            ObservableCollection<Horse> fatherHorses = JsonConvert.DeserializeObject<ObservableCollection<Horse>>(response);

            return fatherHorses;
        }

        public static async Task<bool> AddHorseAsync(string gpk, string nick, string brand, string blodeness, string color, string gend, string dateBirth, string placeBirth, string owner, int motherID, int fatherID, string state)
        {        
            try
            {
                var horseData = new Dictionary<string, string>
                {
                    { "GpkNum", gpk },
                    { "NickName", nick },
                    { "Brand", brand },
                    { "Bloodiness", blodeness },
                    { "Color", color },
                    { "Gender", gend },
                    { "BirthDate", Convert.ToDateTime(dateBirth).ToString("yyyy-MM-dd") },
                    { "BirthPlace", placeBirth },
                    { "Owner", owner },
                    { "MotherID", motherID.ToString() },
                    { "FatherID", fatherID.ToString() },
                    { "State", state },
                };

                var data = new FormUrlEncodedContent(horseData);

                var response = await client.PostAsync("http://1k-horse-base.loc/HorseAccountingApi/horse/horse.php", data);

                var responseString = await response.Content.ReadAsStringAsync();

                return true;


                //TODO: Доделать добавление на стороне сервера


                //DbConnection.CreateConnection();

                //using (var sql = new MySqlConnection(DbConnection.Connection.ConnectionString))
                //{
                //    sql.Open();

                //    MySqlCommand cmd = sql.CreateCommand();
                //    cmd.CommandText = "INSERT INTO `лошадь`(`№ по ГПК`, `Кличка`, `Тавро`, `Кровность`, `Масть`, `Пол`, `Дата рождения`, `Место рождения`, `Владелец`, `Мать`, `Отец`, `Состояние`) " +
                //    "VALUES (@gpk, @nick, @brand, @blodeness, @color, @gend, @date, @place, @owner, @mother, @father, @state)";

                //    cmd.Parameters.AddWithValue("@gpk", gpk);
                //    cmd.Parameters.AddWithValue("@nick", nick);
                //    cmd.Parameters.AddWithValue("@brand", brand);
                //    cmd.Parameters.AddWithValue("@blodeness", blodeness);
                //    cmd.Parameters.AddWithValue("@color", color);
                //    cmd.Parameters.AddWithValue("@gend", gend);
                //    cmd.Parameters.AddWithValue("@date", Convert.ToDateTime(dateBirth).ToString("yyyy-MM-dd"));
                //    cmd.Parameters.AddWithValue("@place", placeBirth);
                //    cmd.Parameters.AddWithValue("@owner", owner);
                //    cmd.Parameters.AddWithValue("@mother", motherID);
                //    cmd.Parameters.AddWithValue("@father", fatherID);
                //    cmd.Parameters.AddWithValue("@state", state);

                //    cmd.ExecuteNonQuery();

                //    sql.Close();

                //    return true;
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public void CleanHorseData()
        {
            GpkNum = string.Empty;
            NickName = string.Empty;
            Brand = string.Empty;
            Bloodiness = string.Empty;
            Color = string.Empty;
            BirthDate = string.Empty;
            BirthPlace = string.Empty;
            Owner = string.Empty;
            MotherID = 0;
            FatherID = 0;
            State = null;
            FullName = null;
        }

        public static int GetLastHorseID()
        {
            DbConnection.CreateConnection();

            int lastHorseID = 0;

            try
            {
                using (var sql = new MySqlConnection(DbConnection.Connection.ConnectionString))
                {
                    sql.Open();

                    MySqlCommand cmd = sql.CreateCommand();
                    cmd.CommandText = "SELECT MAX(ID) FROM `лошадь`";

                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    while (dataReader.Read())
                    {
                        lastHorseID = dataReader.GetInt32(0);
                    }

                    dataReader.Close();

                    sql.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return lastHorseID;
        }

        public static void ChangeHorseState(int id, string state)
        {
            try
            {
                DbConnection.CreateConnection();

                using (var sql = new MySqlConnection(DbConnection.Connection.ConnectionString))
                {
                    sql.Open();

                    MySqlCommand cmd = sql.CreateCommand();
                    cmd.CommandText = "Update `лошадь` set `Состояние` = @state WHERE ID = @id";

                    cmd.Parameters.AddWithValue("@state", state);
                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();

                    sql.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        #endregion

        #region ShowHorsePage



        #endregion

        #region ChangeHorsePage

        public static async Task<Horse> GetSelectedHorseAsync(int ID)
        {
            string url = "http://1k-horse-base.loc/HorseAccountingApi/horse/horse.php?gethorse=" + ID;

            string response = await client.GetStringAsync(url);

            Horse selectedHorse = JsonConvert.DeserializeObject<Horse>(response);

            return selectedHorse;
        }


        public static bool ChangeHorse(int id, string gpk, string nick, string brand, string blodeness, string color, string gend, string dateBirth, string placeBirth, string owner, int motherID, int fatherID)
        {
            try
            {
                DbConnection.CreateConnection();

                using (var sql = new MySqlConnection(DbConnection.Connection.ConnectionString))
                {
                    sql.Open();

                    MySqlCommand cmd = sql.CreateCommand();
                    cmd.CommandText = "Update `лошадь` set `№ по ГПК` = @gpk, `Кличка` = @nick, `Тавро` = @brand, `Кровность` = @blodeness, `Масть` = @color, `Пол` = @gend, `Дата рождения` = @date," +
                        " `Место рождения` = @place, `Владелец` = @owner, `Мать` = @mother, `Отец` = @father WHERE ID = @id";

                    cmd.Parameters.AddWithValue("@gpk", gpk);
                    cmd.Parameters.AddWithValue("@nick", nick);
                    cmd.Parameters.AddWithValue("@brand", brand);
                    cmd.Parameters.AddWithValue("@blodeness", blodeness);
                    cmd.Parameters.AddWithValue("@color", color);
                    cmd.Parameters.AddWithValue("@gend", gend);
                    cmd.Parameters.AddWithValue("@date", Convert.ToDateTime(dateBirth).ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@place", placeBirth);
                    cmd.Parameters.AddWithValue("@owner", owner);
                    cmd.Parameters.AddWithValue("@mother", motherID);
                    cmd.Parameters.AddWithValue("@father", fatherID);
                    cmd.Parameters.AddWithValue("@id", id);

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

        #endregion
    }
}
