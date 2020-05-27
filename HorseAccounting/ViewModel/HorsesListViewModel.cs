using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using HorseAccounting.Infra;
using HorseAccounting.Model;
using HorseAccounting.View;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HorseAccounting.ViewModel
{
    public class HorsesListViewModel : ViewModelBase
    {
        #region Vars

        private IPageNavigationService _navigationService;

        private Horse _horse;
        private Horse _fatherHorse;

        private ObservableCollection<Horse> _horses;
        private ObservableCollection<Horse> _fatherHorseList;

        private ShowHorseViewModel _showHorse;

        private bool _allBtnCheck;
        private bool _actingBtnCheck;
        private bool _retiredBtnCheck;
        private bool _stallionBtnCheck;
        private bool _mareBtnCheck;

        private string _searchQuery;

        About about;
        bool isAboutOpen;

        #endregion

        public HorsesListViewModel(IPageNavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public async void OnPageLoad()
        {
            await Task.Run(() =>
            {
                Console.WriteLine(Properties.Settings.Default.ListState);
                try
                {
                    _fatherHorseList = Horse.GetFatherHorseAsync().Result;
                    RaisePropertyChanged(() => FatherHorseList);
                }
                catch
                {
                    Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Ошибка получения данных! Проверьте ваше интернет соединение."));
                }

                if (string.IsNullOrEmpty(Properties.Settings.Default.ListState))
                {
                    Properties.Settings.Default.ListState = "all";
                    Properties.Settings.Default.Save();
                    GetHorses(Properties.Settings.Default.ListState);
                }
                else
                {
                    switch (Properties.Settings.Default.ListState)
                    {
                        case "all":
                            GetHorses(Properties.Settings.Default.ListState);
                            break;
                        case "acting":
                            GetHorses(Properties.Settings.Default.ListState);
                            break;
                        case "retired":
                            GetHorses(Properties.Settings.Default.ListState);
                            break;
                        case "stallion":
                            GetHorses(Properties.Settings.Default.ListState);
                            break;
                        case "mare":
                            GetHorses(Properties.Settings.Default.ListState);
                            break;
                        default:
                            GetHorses(Properties.Settings.Default.ListState);
                            break;
                    }
                }

                isAboutOpen = true;

            }).ConfigureAwait(true);
        }

        #region GetMethod

        private void GetHorses(string state)
        {
            try
            {
                switch (state)
                {
                    case "all":

                        AllBtnCheck = true;
                        SearchQuery = null;
                        FatherHorse = null;

                        _horses = Horse.GetHorses().Result;
                        RaisePropertyChanged(() => HorsesList);

                        Properties.Settings.Default.ListState = state;
                        Properties.Settings.Default.Save();

                        Console.WriteLine(Properties.Settings.Default.ListState);

                        break;

                    case "acting":

                        ActingBtnCheck = true;
                        SearchQuery = null;
                        FatherHorse = null;

                        _horses = Horse.GetActingHorses().Result;
                        RaisePropertyChanged(() => HorsesList);

                        Properties.Settings.Default.ListState = state;
                        Properties.Settings.Default.Save();

                        Console.WriteLine(Properties.Settings.Default.ListState);

                        break;

                    case "retired":

                        RetiredBtnCheck = true;
                        SearchQuery = null;
                        FatherHorse = null;

                        _horses = Horse.GetRetiredHorses().Result;
                        RaisePropertyChanged(() => HorsesList);

                        Properties.Settings.Default.ListState = state;
                        Properties.Settings.Default.Save();

                        Console.WriteLine(Properties.Settings.Default.ListState);

                        break;

                    case "stallion":

                        StallionBtnCheck = true;
                        SearchQuery = null;
                        FatherHorse = null;

                        _horses = Horse.GetFatherHorseAsync().Result;
                        RaisePropertyChanged(() => HorsesList);

                        Properties.Settings.Default.ListState = state;
                        Properties.Settings.Default.Save();

                        Console.WriteLine(Properties.Settings.Default.ListState);

                        break;

                    case "mare":

                        MareBtnCheck = true;
                        SearchQuery = null;
                        FatherHorse = null;

                        _horses = Horse.GetMotherHorseAsync().Result;
                        RaisePropertyChanged(() => HorsesList);

                        Properties.Settings.Default.ListState = state;
                        Properties.Settings.Default.Save();

                        Console.WriteLine(Properties.Settings.Default.ListState);

                        break;
                    default:
                        if (DateTime.TryParseExact(state, "yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dt))
                        {
                            FatherHorse = null;

                            SearchQuery = state;

                            _horses = Horse.SearchHorsesByYearAsync(SearchQuery).Result;
                            RaisePropertyChanged(() => HorsesList);

                            Properties.Settings.Default.ListState = SearchQuery;
                            Properties.Settings.Default.Save();

                            Console.WriteLine(Properties.Settings.Default.ListState);

                            break;

                        }
                        else
                        {
                            FatherHorse = null;

                            SearchQuery = state;

                            _horses = Horse.SearchHorsesAsync(SearchQuery).Result;
                            RaisePropertyChanged(() => HorsesList);

                            Properties.Settings.Default.ListState = SearchQuery;
                            Properties.Settings.Default.Save();

                            Console.WriteLine(Properties.Settings.Default.ListState);

                            break;
                        }
                }
            }
            catch
            {
                Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Ошибка получения данных! Проверьте ваше интернет соединение или обратитесь к разработчику."));
            }
        }

        #endregion

        public async void OnSelectionChanged()
        {
            await Task.Run(() =>
            {
                try
                {
                    if (FatherHorse != null)
                    {
                        AllBtnCheck = false;
                        ActingBtnCheck = false;
                        RetiredBtnCheck = false;
                        StallionBtnCheck = false;
                        MareBtnCheck = false;

                        _horses = Horse.SearchByFatherHorseAsync(FatherHorse.ID).Result;
                        RaisePropertyChanged(() => HorsesList);
                    }
                    else
                    {
                        Properties.Settings.Default.ListState = "all";
                        Properties.Settings.Default.Save();

                        GetHorses(Properties.Settings.Default.ListState);
                    }
                }
                catch
                {
                    Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Ошибка получения данных! Проверьте ваше интернет соединение или обратитесь к разработчику."));
                }
            }).ConfigureAwait(true);
        }

        public async void OnSearch()
        {
            await Task.Run(() =>
            {
                if (!string.IsNullOrEmpty(SearchQuery))
                {
                    try
                    {
                        AllBtnCheck = false;
                        ActingBtnCheck = false;
                        RetiredBtnCheck = false;
                        StallionBtnCheck = false;
                        MareBtnCheck = false;

                        GetHorses(SearchQuery);
                    }
                    catch
                    {
                        Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Ошибка получения данных! Проверьте ваше интернет соединение или обратитесь к разработчику."));
                    }
                }
                else
                {
                    Properties.Settings.Default.ListState = "all";
                    Properties.Settings.Default.Save();

                    GetHorses(Properties.Settings.Default.ListState);
                }
            }).ConfigureAwait(true);
        }

        #region Definitions

        public bool AllBtnCheck
        {
            get
            {
                return _allBtnCheck;
            }
            set
            {
                _allBtnCheck = value;
                RaisePropertyChanged(nameof(AllBtnCheck));
            }
        }

        public bool ActingBtnCheck
        {
            get
            {
                return _actingBtnCheck;
            }

            set
            {
                _actingBtnCheck = value;
                RaisePropertyChanged(nameof(ActingBtnCheck));
            }
        }

        public bool RetiredBtnCheck
        {
            get
            {
                return _retiredBtnCheck;
            }

            set
            {
                _retiredBtnCheck = value;
                RaisePropertyChanged(nameof(RetiredBtnCheck));
            }
        }

        public bool StallionBtnCheck
        {
            get
            {
                return _stallionBtnCheck;
            }

            set
            {
                _stallionBtnCheck = value;
                RaisePropertyChanged(nameof(StallionBtnCheck));
            }
        }

        public bool MareBtnCheck
        {
            get
            {
                return _mareBtnCheck;
            }

            set
            {
                _mareBtnCheck = value;
                RaisePropertyChanged(nameof(MareBtnCheck));
            }
        }

        public ObservableCollection<Horse> HorsesList
        {
            get
            {
                return _horses;
            }
        }

        public ObservableCollection<Horse> FatherHorseList
        {
            get
            {
                return _fatherHorseList;
            }
        }

        public string SearchQuery
        {
            get
            {
                return _searchQuery;
            }

            set
            {

                _searchQuery = value;
                RaisePropertyChanged(nameof(SearchQuery));
            }
        }

        public Horse SelectedHorse
        {
            get
            {
                return _horse;
            }

            set
            {
                _horse = value;
                RaisePropertyChanged(nameof(SelectedHorse));
            }
        }

        public Horse FatherHorse
        {
            get
            {
                return _fatherHorse;
            }

            set
            {
                _fatherHorse = value;
                RaisePropertyChanged(nameof(FatherHorse));
            }
        }

        public ShowHorseViewModel ShowHorse
        {
            get
            {
                return _showHorse;
            }

            set
            {
                if (_showHorse != null)
                {
                    _showHorse = value;
                    RaisePropertyChanged(nameof(ShowHorse));
                }
            }
        }

        #endregion

        #region MenuCommands

        private RelayCommand _openDocFolder;

        public RelayCommand OpenDocFolder
        {
            get
            {
                return _openDocFolder
                    ?? (_openDocFolder = new RelayCommand(
                    () =>
                    {
                        Process.Start("explorer.exe", @"C:\Users\Public\Documents\Помощник коневода\");
                    }));
            }

            private set
            {
                _openDocFolder = value;
            }
        }

        private RelayCommand _openTaleMagazine;

        public RelayCommand OpenTaleMagazine
        {
            get
            {
                return _openTaleMagazine
                    ?? (_openTaleMagazine = new RelayCommand(
                    () =>
                    {
                        _navigationService.NavigateTo("TaleMagazinePage");
                    }));
            }

            private set
            {
                _openTaleMagazine = value;
            }
        }

        private RelayCommand _openMagazineFolder;

        public RelayCommand OpenMagazineFolder
        {
            get
            {
                return _openMagazineFolder
                    ?? (_openMagazineFolder = new RelayCommand(
                    () =>
                    {
                        Process.Start("explorer.exe", @"C:\Users\Public\Documents\Помощник коневода\Журналы случки\");
                    }));
            }

            private set
            {
                _openMagazineFolder = value;
            }
        }

        private RelayCommand _openAbout;

        public RelayCommand OpenAbout
        {
            get
            {
                return _openAbout
                    ?? (_openAbout = new RelayCommand(
                    () =>
                    {
                        FormCollection fc = Application.OpenForms;
                        for(int i = 0; i < fc.Count; i++)
                        {
                            if(fc[i].Name == "About")
                            {
                                fc[i].Close();
                                isAboutOpen = true;
                            }
                        }

                        if (isAboutOpen)
                        {
                            about = new About();
                            about.Show();
                        }
                    }));
            }

            private set
            {
                _openAbout = value;
            }
        }

        private RelayCommand _openHelp;

        public RelayCommand OpenHelp
        {
            get
            {
                return _openHelp
                    ?? (_openHelp = new RelayCommand(
                    () =>
                    {
                        var proc = new System.Diagnostics.Process();
                        proc.StartInfo.FileName = System.IO.Path.GetFullPath(@"Files\Help\help.chm"); ;
                        proc.StartInfo.UseShellExecute = true;
                        proc.Start();
                    }));
            }

            private set
            {
                _openHelp = value;
            }
        }

        private RelayCommand _showDiagrams;

        public RelayCommand ShowDiagrams
        {
            get
            {
                return _showDiagrams
                    ?? (_showDiagrams = new RelayCommand(
                    () =>
                    {
                        _navigationService.NavigateTo("ShowDiagramsPage");
                    }));
            }

            private set
            {
                _showDiagrams = value;
            }
        }

        #endregion

        #region RadioButtons

        private RelayCommand _showAllHorses;

        public RelayCommand ShowAllHorses
        {
            get
            {
                return _showAllHorses
                    ?? (_showAllHorses = new RelayCommand(
                    async () =>
                    {
                        await Task.Run(() =>
                        {
                            try
                            {
                                GetHorses("all");
                            }
                            catch
                            {
                                Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Ошибка получения данных! Проверьте ваше интернет соединение или обратитесь к разработчику."));
                            }
                        }).ConfigureAwait(true);
                    }));
            }

            private set
            {
                _showAllHorses = value;
            }
        }

        private RelayCommand _showActingHorses;

        public RelayCommand ShowActingHorses
        {
            get
            {
                return _showActingHorses
                    ?? (_showActingHorses = new RelayCommand(
                    async () =>
                    {
                        await Task.Run(() =>
                        {
                            try
                            {
                                GetHorses("acting");
                            }
                            catch
                            {
                                Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Ошибка получения данных! Проверьте ваше интернет соединение или обратитесь к разработчику."));
                            }
                        }).ConfigureAwait(true);
                    }));
            }

            private set
            {
                _showActingHorses = value;
            }
        }

        private RelayCommand _showRetiredHorses;

        public RelayCommand ShowRetiredHorses
        {
            get
            {
                return _showRetiredHorses
                    ?? (_showRetiredHorses = new RelayCommand(
                    async () =>
                    {
                        await Task.Run(() =>
                        {
                            try
                            {

                                GetHorses("retired");
                            }
                            catch
                            {
                                Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Ошибка получения данных! Проверьте ваше интернет соединение или обратитесь к разработчику."));
                            }
                        }).ConfigureAwait(true);
                    }));
            }

            private set
            {
                _showRetiredHorses = value;
            }
        }

        private RelayCommand _showStallionHorses;

        public RelayCommand ShowStallionHorses
        {
            get
            {
                return _showStallionHorses
                    ?? (_showStallionHorses = new RelayCommand(
                    async () =>
                    {
                        await Task.Run(() =>
                        {
                            try
                            {
                                GetHorses("stallion");
                            }
                            catch
                            {
                                Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Ошибка получения данных! Проверьте ваше интернет соединение или обратитесь к разработчику."));
                            }
                        }).ConfigureAwait(true);
                    }));
            }

            private set
            {
                _showStallionHorses = value;
            }
        }

        private RelayCommand _showMareHorses;

        public RelayCommand ShowMareHorses
        {
            get
            {
                return _showMareHorses
                    ?? (_showMareHorses = new RelayCommand(
                    async () =>
                    {
                        await Task.Run(() =>
                        {
                            try
                            {
                                GetHorses("mare");
                            }
                            catch
                            {
                                Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Ошибка получения данных! Проверьте ваше интернет соединение или обратитесь к разработчику."));
                            }
                        }).ConfigureAwait(true);
                    }));
            }

            private set
            {
                _showMareHorses = value;
            }
        }

        #endregion

        #region WindowCommands

        private RelayCommand _addHorse;

        public RelayCommand AddHorse
        {
            get
            {
                return _addHorse
                    ?? (_addHorse = new RelayCommand(
                    () =>
                    {
                        _navigationService.NavigateTo("AddHorsePage");
                    }));
            }

            private set
            {
                _addHorse = value;
            }
        }

        //private RelayCommand _searchHorse;

        // public RelayCommand SearchHorse
        // {
        //     get
        //     {
        //         return _searchHorse
        //             ?? (_searchHorse = new RelayCommand(
        //             () =>
        //             {
        //                 _horses = Horse.SearchHorses(SearchQuery);
        //                 RaisePropertyChanged(() => HorsesList);
        //             }));
        //     }
        // }

        #endregion

        public void DoubleClickMethod()
        {
            if (SelectedHorse != null)
            {
                if (SelectedHorse.ID != 0)
                {
                    _navigationService.NavigateTo("ShowHorsePage", SelectedHorse);
                }
            }
        }
    }
}
