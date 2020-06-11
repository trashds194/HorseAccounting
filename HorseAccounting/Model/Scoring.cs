using GalaSoft.MvvmLight;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;

namespace HorseAccounting.Model
{
    public class Scoring : ObservableObject
    {
        #region Vars

        private int _id;
        private string _date;
        private string _age;
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

        private static readonly HttpClient client = new HttpClient();

        private static readonly string api = Properties.Resources.ru;

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

        public string Age
        {
            get { return _age; }
            set { Set<string>(() => Age, ref _age, value); }
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

        #endregion

        #region ShowHorsePage

        public static async Task<ObservableCollection<Scoring>> GetSelectedScoring(int iD)
        {
            string url = api + "scoring.php?scoring=" + iD;

            string response = client.GetStringAsync(url).GetAwaiter().GetResult();

            ObservableCollection<Scoring> selectedScoring = JsonConvert.DeserializeObject<ObservableCollection<Scoring>>(response);

            return selectedScoring;
        }

        #endregion

        #region AddScoringPage

        public static async Task<bool> AddScoringAsync(string date, string age, string boniter, int origin, int typicality, int measure, int exterior, int workingCapacity, int offspringQuality, string theClass, string comment, int horseID)
        {
            var scoringData = new Dictionary<string, string>
                {
                    { "Date", Convert.ToDateTime(date).ToString("yyyy-MM-dd") },
                    { "Age", age },
                    { "Boniter", boniter },
                    { "Origin", origin.ToString() },
                    { "Typicality",typicality.ToString() },
                    { "Measurements", measure.ToString() },
                    { "Exterior", exterior.ToString() },
                    { "WorkingCapacity", workingCapacity.ToString() },
                    { "OffspringQuality", offspringQuality.ToString() },
                    { "TheClass", theClass.ToString() },
                    { "Comment", comment },
                    { "HorseID", horseID.ToString() }
                };

            var data = new FormUrlEncodedContent(scoringData);

            var response = client.PostAsync(api + "scoring.php?scoring=add", data).GetAwaiter().GetResult();

            var responseString = await response.Content.ReadAsStringAsync();

            Console.WriteLine(responseString);

            return true;
        }

        public void CleanScoringData()
        {
            Date = string.Empty;
            Age = string.Empty;
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