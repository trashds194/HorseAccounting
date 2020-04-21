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
        private string _fatherFullName;
        private string _fatherBreed;
        private string _foalClass;
        private string _foalDate;
        private string _foalGender;
        private string _foalColor;
        private string _foalNickName;
        private string _foalDestination;
        private int _fatherID;
        private int _foalID;
        private int _motherID;

        private static readonly HttpClient client = new HttpClient();

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

        public string FoalClass
        {
            get { return _foalClass; }
            set { Set<string>(() => FoalClass, ref _foalClass, value); }
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

        #region ShowHorsePage

        public static async Task<ObservableCollection<TribalUse>> GetSelectedTribalUse(int iD)
        {
            string url = "http://1k-horse-base.loc/HorseAccountingApi/tribaluse.php?tribaluse=" + iD;

            string response = client.GetStringAsync(url).GetAwaiter().GetResult();

            ObservableCollection<TribalUse> selectedTribalUse = JsonConvert.DeserializeObject<ObservableCollection<TribalUse>>(response);

            return selectedTribalUse;
        }

        #endregion
    }
}
