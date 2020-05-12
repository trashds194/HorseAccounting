﻿using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
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

        private static readonly string api = Properties.Resources.loc;

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
            string url = api + "progression.php?progression=" + iD;

            string response = client.GetStringAsync(url).GetAwaiter().GetResult();

            ObservableCollection<Progression> selectedProgression = JsonConvert.DeserializeObject<ObservableCollection<Progression>>(response);

            return selectedProgression;
        }

        #endregion

        #region AddHorsePage

        public static async Task<bool> AddProgressionAsync(string date, string destination, string comment, int horseID)
        {
            try
            {
                var progressionData = new Dictionary<string, string>
                {
                    { "Date", Convert.ToDateTime(date).ToString("yyyy-MM-dd") },
                    { "Destination", destination },
                    { "Comment", comment },
                    { "HorseID", horseID.ToString() }
                };

                var data = new FormUrlEncodedContent(progressionData);

                var response = client.PostAsync(api + "progression.php?progression=add", data).GetAwaiter().GetResult();

                var responseString = await response.Content.ReadAsStringAsync();

                Console.WriteLine(responseString);

                return true;
            }
            catch (Exception ex)
            {
                if (ex is FormatException)
                {
                    Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Вы не выбрали дату рождения лошади!"));
                    return false;
                }
                else if (ex is HttpRequestException || ex is SocketException || ex is WebException || ex is AggregateException)
                {
                    Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Ошибка добавления данных! Проверьте ваше интернет соединение или обратитесь к разработчику."));
                    return false;
                }
                throw;
            }
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
