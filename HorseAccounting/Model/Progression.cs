using GalaSoft.MvvmLight;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
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

        private static readonly HttpClient client = new HttpClient();

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

        #endregion

        #region ShowHorsePage

        public static async Task<ObservableCollection<Progression>> GetSelectedProgression(int iD)
        {
            string url = "http://1k-horse-base.ru/api/progression.php?progression=" + iD;

            string response = client.GetStringAsync(url).GetAwaiter().GetResult();

            ObservableCollection<Progression> selectedProgression = JsonConvert.DeserializeObject<ObservableCollection<Progression>>(response);

            return selectedProgression;
        }

        #endregion

        #region AddHorsePage

        public static async Task<bool> AddProgressionAsync(string date, string destination, string comment, int horseID)
        {
            var progressionData = new Dictionary<string, string>
                {
                    { "Date", Convert.ToDateTime(date).ToString("yyyy-MM-dd") },
                    { "Destination", destination },
                    { "Comment", comment },
                    { "HorseID", horseID.ToString() }
                };

            var data = new FormUrlEncodedContent(progressionData);

            var response = client.PostAsync("http://1k-horse-base.ru/api/progression.php?progression=add", data).GetAwaiter().GetResult();

            var responseString = await response.Content.ReadAsStringAsync();

            Console.WriteLine(responseString);

            return true;
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
