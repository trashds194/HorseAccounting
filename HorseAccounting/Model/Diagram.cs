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
    public class Diagram : ObservableObject
    {
        #region Vars

        private string _title;
        private int _value;
        private string _year;

        private static readonly HttpClient client = new HttpClient();

        private static readonly string api = Properties.Resources.ru;

        #endregion

        #region Definitions

        public string Title
        {
            get { return _title; }
            set { Set<string>(() => Title, ref _title, value); }
        }

        public int Value
        {
            get { return _value; }
            set { Set<int>(() => Value, ref _value, value); }
        }

        public string Year
        {
            get { return _year; }
            set { Set<string>(() => Year, ref _year, value); }
        }

        #endregion

        #region ShowDiagramsPage

        public static async Task<ObservableCollection<Diagram>> GetGenderDiagram()
        {
            string url = api + "diagram.php?diagram=gender";

            string response = client.GetStringAsync(url).GetAwaiter().GetResult();

            ObservableCollection<Diagram> mareDiagram = JsonConvert.DeserializeObject<ObservableCollection<Diagram>>(response);

            return mareDiagram;
        }

        public static async Task<ObservableCollection<Diagram>> GetBirthPlaceDiagram()
        {
            string url = api + "diagram.php?diagram=birth-place";

            string response = client.GetStringAsync(url).GetAwaiter().GetResult();

            ObservableCollection<Diagram> birthPlaceDiagram = JsonConvert.DeserializeObject<ObservableCollection<Diagram>>(response);

            return birthPlaceDiagram;
        }

        public static async Task<ObservableCollection<Diagram>> GetStallionYearDiagram()
        {
            string url = api + "diagram.php?diagram=stallion-year";

            string response = client.GetStringAsync(url).GetAwaiter().GetResult();

            ObservableCollection<Diagram> birthPlaceDiagram = JsonConvert.DeserializeObject<ObservableCollection<Diagram>>(response);

            return birthPlaceDiagram;
        }

        public static async Task<ObservableCollection<Diagram>> GetMareYearDiagram()
        {
            string url = api + "diagram.php?diagram=mare-year";

            string response = client.GetStringAsync(url).GetAwaiter().GetResult();

            ObservableCollection<Diagram> birthPlaceDiagram = JsonConvert.DeserializeObject<ObservableCollection<Diagram>>(response);

            return birthPlaceDiagram;
        }

        #endregion
    }
}
