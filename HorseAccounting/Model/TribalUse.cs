using GalaSoft.MvvmLight;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HorseAccounting.Model
{
    public class TribalUse : ObservableObject
    {
        #region Vars

        private int _id;
        private string _year;
        private string _lastDate;
        private string _matingType;
        private string _fatherFullName;
        private string _fatherBreed;
        private string _fatherClass;
        private string _foalDate;
        private string _foalGender;
        private string _foalColor;
        private string _foalNickName;
        private string _foalBrand;
        private string _foalDestination;
        private int _fatherID;
        private int _foalID;
        private int _motherID;

        private static readonly HttpClient client = new HttpClient();

        private static readonly string link = "ru";

        #endregion

        #region Definitions

        public int ID
        {
            get { return _id; }
            set { Set<int>(() => ID, ref _id, value); }
        }

        public string Year
        {
            get { return _year; }
            set { Set<string>(() => Year, ref _year, value); }
        }

        public string LastDate
        {
            get { return _lastDate; }
            set { Set<string>(() => LastDate, ref _lastDate, value); }
        }

        public string MatingType
        {
            get { return _matingType; }
            set { Set<string>(() => MatingType, ref _matingType, value); }
        }

        public string FatherFullName
        {
            get { return _fatherFullName; }
            set { Set<string>(() => FatherFullName, ref _fatherFullName, value); }
        }

        public string FatherBreed
        {
            get { return _fatherBreed; }
            set { Set<string>(() => FatherBreed, ref _fatherBreed, value); }
        }

        public string FatherClass
        {
            get { return _fatherClass; }
            set { Set<string>(() => FatherClass, ref _fatherClass, value); }
        }

        public string FoalDate
        {
            get { return _foalDate; }
            set { Set<string>(() => FoalDate, ref _foalDate, value); }
        }

        public string FoalGender
        {
            get { return _foalGender; }
            set { Set<string>(() => FoalGender, ref _foalGender, value); }
        }

        public string FoalColor
        {
            get { return _foalColor; }
            set { Set<string>(() => FoalColor, ref _foalColor, value); }
        }

        public string FoalNickName
        {
            get { return _foalNickName; }
            set { Set<string>(() => FoalNickName, ref _foalNickName, value); }
        }

        public string FoalBrand
        {
            get { return _foalBrand; }
            set { Set<string>(() => FoalBrand, ref _foalBrand, value); }
        }

        public string FoalDestination
        {
            get { return _foalDestination; }
            set { Set<string>(() => FoalDestination, ref _foalDestination, value); }
        }

        public int FatherID
        {
            get { return _fatherID; }
            set { Set<int>(() => FatherID, ref _fatherID, value); }
        }

        public int FoalID
        {
            get { return _foalID; }
            set { Set<int>(() => FoalID, ref _foalID, value); }
        }

        public int MotherID
        {
            get { return _motherID; }
            set { Set<int>(() => MotherID, ref _motherID, value); }
        }

        #endregion

        public void CleanTribalUseData()
        {
            Year = string.Empty;
            LastDate = string.Empty;
            MatingType = string.Empty;
            FatherFullName = string.Empty;
            FatherBreed = string.Empty;
            FatherClass = string.Empty;
            FoalDate = string.Empty;
            FoalColor = string.Empty;
            FoalNickName = string.Empty;
            FoalBrand = string.Empty;
            FoalDestination = string.Empty;
            FatherID = 0;
        }

        #region ShowHorsePage

        public static async Task<ObservableCollection<TribalUse>> GetSelectedTribalUse(int iD)
        {
            string url = "http://1k-horse-base." + link + "/api/tribaluse.php?tribaluse=" + iD;

            string response = client.GetStringAsync(url).GetAwaiter().GetResult();

            ObservableCollection<TribalUse> selectedTribalUse = JsonConvert.DeserializeObject<ObservableCollection<TribalUse>>(response);

            return selectedTribalUse;
        }

        #endregion

        #region AddTribalUsePage

        public static async Task<bool> AddTribalUseAsync(string year, string lastDate, string matingType, string fatherFullName, string fatherBreed, string fatherClass,
            string foalDate, string foalGender, string foalColor, string foalNickName, string foalBrand, string foalDestination, int fatherID, int foalID, int motherID)
        {
            var tribalUseData = new Dictionary<string, string>
                {
                    { "Year", year },
                    { "LastDate", Convert.ToDateTime(lastDate).ToString("yyyy-MM-dd") },
                    { "MatingType", matingType },
                    { "FatherFullName", fatherFullName },
                    { "FatherBreed", fatherBreed },
                    { "FatherClass", fatherClass },
                    { "FoalDate", foalDate },
                    { "FoalGender", foalGender },
                    { "FoalColor", foalColor },
                    { "FoalNickName", foalNickName },
                    { "FoalBrand", foalBrand },
                    { "FoalDestination", foalDestination },
                    { "FatherID", fatherID.ToString() },
                    { "FoalID", foalID.ToString() },
                    { "MotherID", motherID.ToString() }
                };

            var data = new FormUrlEncodedContent(tribalUseData);

            var response = client.PostAsync("http://1k-horse-base." + link + "/api/tribaluse.php?tribaluse=add", data).GetAwaiter().GetResult();

            var responseString = await response.Content.ReadAsStringAsync();

            Console.WriteLine(responseString);

            return true;
        }

        #endregion

        #region ChangeHorsePage

        public static async Task<bool> ChangeTribalUseAsync(string foalDate, string foalGender, string foalColor, string foalNickName, string foalBrand, int fatherID, int foalID, int motherID)
        {
            try
            {
                var tribalUseData = new Dictionary<string, string>
                {
                    { "FoalDate", foalDate },
                    { "FoalGender", foalGender },
                    { "FoalColor", foalColor },
                    { "FoalNickName", foalNickName },
                    { "FoalBrand", foalBrand },
                    { "FatherID", fatherID.ToString() },
                    { "FoalID", foalID.ToString() },
                    { "MotherID", motherID.ToString() }
                };

                var data = new FormUrlEncodedContent(tribalUseData);

                var response = client.PostAsync("http://1k-horse-base." + link + "/api/tribaluse.php?tribaluse=change", data).GetAwaiter().GetResult();

                var responseString = await response.Content.ReadAsStringAsync();

                Console.WriteLine(responseString);

                if (Convert.ToInt32(responseString) > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        #endregion
    }
}
