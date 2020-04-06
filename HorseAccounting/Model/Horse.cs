﻿using GalaSoft.MvvmLight;
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
            string url = "http://1k-horse-base.loc/HorseAccountingApi/horse.php?horse=all";

            string response = client.GetStringAsync(url).GetAwaiter().GetResult();

            ObservableCollection<Horse> horses = JsonConvert.DeserializeObject<ObservableCollection<Horse>>(response);

            return horses;
        }

        public static async Task<ObservableCollection<Horse>> GetActingHorses()
        {
            string url = "http://1k-horse-base.loc/HorseAccountingApi/horse.php?horse=acting";

            string response = client.GetStringAsync(url).GetAwaiter().GetResult();

            ObservableCollection<Horse> horses = JsonConvert.DeserializeObject<ObservableCollection<Horse>>(response);

            return horses;
        }

        public static async Task<ObservableCollection<Horse>> GetRetiredHorses()
        {
            string url = "http://1k-horse-base.loc/HorseAccountingApi/horse.php?horse=retired";

            string response = client.GetStringAsync(url).GetAwaiter().GetResult();

            ObservableCollection<Horse> horses = JsonConvert.DeserializeObject<ObservableCollection<Horse>>(response);

            return horses;
        }

        public static async Task<ObservableCollection<Horse>> SearchHorsesAsync(string searchQuery)
        {
            string url = "http://1k-horse-base.loc/HorseAccountingApi/horse.php?search=" + searchQuery;

            string response = client.GetStringAsync(url).GetAwaiter().GetResult();

            ObservableCollection<Horse> searchedHorses = JsonConvert.DeserializeObject<ObservableCollection<Horse>>(response);

            return searchedHorses;
        }

        #endregion

        #region AddHorsePage

        public static async Task<ObservableCollection<Horse>> GetMotherHorseAsync()
        {
            string url = "http://1k-horse-base.loc/HorseAccountingApi/horse.php?horse=mother";

            string response = client.GetStringAsync(url).GetAwaiter().GetResult();

            ObservableCollection<Horse> motherHorses = JsonConvert.DeserializeObject<ObservableCollection<Horse>>(response);

            return motherHorses;
        }

        public static async Task<ObservableCollection<Horse>> GetFatherHorseAsync()
        {
            string url = "http://1k-horse-base.loc/HorseAccountingApi/horse.php?horse=father";

            string response = client.GetStringAsync(url).GetAwaiter().GetResult();

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

                var response = client.PostAsync("http://1k-horse-base.loc/HorseAccountingApi/horse.php?horse=add", data).GetAwaiter().GetResult();

                var responseString = await response.Content.ReadAsStringAsync();

                Console.WriteLine(responseString);

                return true;
            }
            catch
            {
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

        public static async Task<int> GetLastHorseIDAsync()
        {
            string url = "http://1k-horse-base.loc/HorseAccountingApi/horse.php?horse=last-id";

            string response = client.GetStringAsync(url).GetAwaiter().GetResult();

            Horse lastHorse = JsonConvert.DeserializeObject<Horse>(response);

            return Convert.ToInt32(lastHorse.ID);
        }

        public static async Task ChangeHorseStateAsync(int id, string state)
        {
            var horseData = new Dictionary<string, string>
                {
                    { "ID", id.ToString() },
                    { "State", state },
                };

            var data = new FormUrlEncodedContent(horseData);

            var response = client.PostAsync("http://1k-horse-base.loc/HorseAccountingApi/horse.php?horse=change-state", data).GetAwaiter().GetResult();

            var responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(true);

            Console.WriteLine(responseString);
        }

        #endregion

        #region ShowHorsePage



        #endregion

        #region ChangeHorsePage

        public static async Task<Horse> GetSelectedHorseAsync(int ID)
        {
            string url = "http://1k-horse-base.loc/HorseAccountingApi/horse.php?horse=" + ID;

            string response = client.GetStringAsync(url).GetAwaiter().GetResult();

            Horse selectedHorse = JsonConvert.DeserializeObject<Horse>(response);

            return selectedHorse;
        }


        public static async Task<bool> ChangeHorseAsync(int id, string gpk, string nick, string brand, string blodeness, string color, string gend, string dateBirth, string placeBirth, string owner, int motherID, int fatherID)
        {
            try
            {
                var horseData = new Dictionary<string, string>
                {
                    { "ID", id.ToString() },
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
                };

                var data = new FormUrlEncodedContent(horseData);

                var response = client.PostAsync("http://1k-horse-base.loc/HorseAccountingApi/horse.php?horse=change", data).GetAwaiter().GetResult();

                var responseString = await response.Content.ReadAsStringAsync();

                Console.WriteLine(responseString);

                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion
    }
}
