using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using HorseAccounting.Infra;
using HorseAccounting.Model;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

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

        private bool _stallionBtnCheck;
        private bool _mareBtnCheck;

        private string _searchQuery;

        #endregion

        public HorsesListViewModel(IPageNavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public async void OnPageLoad()
        {
            await Task.Run(() =>
            {
                SearchQuery = null;
                StallionBtnCheck = false;
                MareBtnCheck = false;
                try
                {
                    _horses = Horse.GetHorses().Result;
                    _fatherHorseList = Horse.GetFatherHorseAsync().Result;
                    RaisePropertyChanged(() => HorsesList);
                    RaisePropertyChanged(() => FatherHorseList);
                }
                catch
                {
                    Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Ошибка получения данных! Проверьте ваше интернет соединение."));
                }
            }).ConfigureAwait(true);
        }

        public async void OnSelectionChanged()
        {
            await Task.Run(() =>
            {
                try
                {
                    if (FatherHorse != null)
                    {
                        _horses = Horse.SearchByFatherHorseAsync(FatherHorse.ID).Result;
                        RaisePropertyChanged(() => HorsesList);
                    }
                    else
                    {
                        _horses = Horse.GetHorses().Result;
                        RaisePropertyChanged(() => HorsesList);
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
                try
                {
                    _horses = Horse.SearchHorsesAsync(SearchQuery).Result;
                    RaisePropertyChanged(() => HorsesList);
                }
                catch
                {
                    Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Ошибка получения данных! Проверьте ваше интернет соединение или обратитесь к разработчику."));
                }
            }).ConfigureAwait(true);
        }

        #region Definitions

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

        private RelayCommand _showAbout;

        public RelayCommand ShowAbout
        {
            get
            {
                return _showAbout
                    ?? (_showAbout = new RelayCommand(
                    () =>
                    {

                    }));
            }

            private set
            {
                _showAbout = value;
            }
        }

        #endregion

        #region RadioButtons

        private RelayCommand _showStallionHorses;

        public RelayCommand ShowStallionHorses
        {
            get
            {
                return _showStallionHorses
                    ?? (_showStallionHorses = new RelayCommand(
                    async () =>
                    {
                        StallionBtnCheck = true;
                        await Task.Run(() =>
                        {
                            try
                            {
                                _horses = Horse.GetFatherHorseAsync().Result;
                                RaisePropertyChanged(() => HorsesList);
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
                        MareBtnCheck = true;
                        await Task.Run(() =>
                        {
                            try
                            {
                                _horses = Horse.GetMotherHorseAsync().Result;
                                RaisePropertyChanged(() => HorsesList);
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

        private RelayCommand _showAllHorses;

        public RelayCommand ShowAllHorses
        {
            get
            {
                return _showAllHorses
                    ?? (_showAllHorses = new RelayCommand(
                    async () =>
                    {
                        StallionBtnCheck = false;
                        MareBtnCheck = false;
                        await Task.Run(() =>
                        {
                            try
                            {
                                FatherHorse = null;
                                _horses = Horse.GetHorses().Result;
                                RaisePropertyChanged(() => HorsesList);
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
                                _horses = Horse.GetActingHorses().Result;
                                RaisePropertyChanged(() => HorsesList);
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
                                _horses = Horse.GetRetiredHorses().Result;
                                RaisePropertyChanged(() => HorsesList);
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

        //public RelayCommand SearchHorse
        //{
        //    get
        //    {
        //        return _searchHorse
        //            ?? (_searchHorse = new RelayCommand(
        //            () =>
        //            {
        //                _horses = Horse.SearchHorses(SearchQuery);
        //                RaisePropertyChanged(() => HorsesList);
        //            }));
        //    }
        //}

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
