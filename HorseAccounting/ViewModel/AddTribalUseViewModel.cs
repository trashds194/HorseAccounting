using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using HorseAccounting.Infra;
using HorseAccounting.Model;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Media3D;

namespace HorseAccounting.ViewModel
{
    public class AddTribalUseViewModel : ViewModelBase
    {
        #region Vars

        private IPageNavigationService _navigationService = new PageNavigationService();

        private Horse _mainHorse;
        private Horse _selectedHorse;
        private Horse _fatherHorse;

        private bool _singleIsChecked;
        private bool _singleIsEnabled;

        private bool _notCoveredIsChecked;
        private bool _notCoveredIsEnabled;

        private int _foalID;

        private TribalUse _addedTribalUse;

        private ObservableCollection<Horse> _fatherHorseList;

        #endregion

        public AddTribalUseViewModel(IPageNavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public async void OnPageLoad()
        {
            await Task.Run(() =>
            {
                try
                {
                    MainHorse = (Horse)_navigationService.Parameter;
                    SelectedHorse = Horse.GetSelectedHorseAsync(MainHorse.ID).Result;
                    ComboBoxesUpdate();
                }
                catch (Exception ex)
                {
                    if (ex is HttpRequestException || ex is SocketException || ex is WebException || ex is AggregateException)
                    {
                        Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Ошибка получения данных! Проверьте ваше интернет соединение или обратитесь к разработчику."));
                    }
                }
                SingleIsChecked = false;
                SingleIsEnabled = true;

                NotCoveredIsChecked = false;
                NotCoveredIsEnabled = true;
            }).ConfigureAwait(true);
        }

        private async void ComboBoxesUpdate()
        {
            await Task.Run(() =>
            {
                try
                {
                    _fatherHorseList = Horse.GetFatherHorseAsync().Result;
                    RaisePropertyChanged(() => FatherHorseList);
                }
                catch (Exception ex)
                {
                    if (ex is HttpRequestException || ex is SocketException || ex is WebException || ex is AggregateException)
                    {
                        Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Ошибка получения данных! Проверьте ваше интернет соединение или обратитесь к разработчику."));
                    }
                }
            }).ConfigureAwait(true);
        }

        private void AddTribalUse()
        {
            try
            {
                if (TribalUse.AddTribalUseAsync(AddedTribalUse.Year, AddedTribalUse.LastDate, FatherHorse.FullName, AddedTribalUse.FatherBreed, AddedTribalUse.FatherClass,
                    AddedTribalUse.FoalDate, AddedTribalUse.FoalGender, AddedTribalUse.FoalColor, AddedTribalUse.FoalNickName, AddedTribalUse.FoalDestination, FatherHorse.ID, FoalID, SelectedHorse.ID).Result)
                {
                    Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Вы успешно добавили племенную деятельность"));
                    AddedTribalUse.CleanTribalUseData();
                }
                else
                {
                    Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Не удалось добавить племенную деятельность"));
                }
            }
            catch (Exception ex)
            {
                if (ex is HttpRequestException || ex is SocketException || ex is WebException || ex is AggregateException)
                {
                    Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Ошибка отправки данных! Проверьте ваше интернет соединение или обратитесь к разработчику."));
                }
            }
        }

        #region Definitions

        public Horse MainHorse
        {
            get
            {
                return _mainHorse;
            }

            set
            {
                if (_mainHorse != value)
                {
                    _mainHorse = value;
                    RaisePropertyChanged(nameof(MainHorse));
                }
            }
        }

        public Horse SelectedHorse
        {
            get
            {
                return _selectedHorse;
            }

            set
            {
                _selectedHorse = value;
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

        public bool SingleIsChecked
        {
            get
            {
                return _singleIsChecked;
            }

            set
            {
                _singleIsChecked = value;
                RaisePropertyChanged(nameof(SingleIsChecked));
                if (SingleIsChecked == true)
                {
                    NotCoveredIsChecked = false;
                    SingleIsEnabled = false;
                    RaisePropertyChanged(nameof(NotCoveredIsChecked));
                    RaisePropertyChanged(nameof(SingleIsEnabled));
                }
                else
                {
                    SingleIsEnabled = true;
                    RaisePropertyChanged(nameof(SingleIsEnabled));
                }
            }
        }

        public bool SingleIsEnabled
        {
            get
            {
                return _singleIsEnabled;
            }

            set
            {
                _singleIsEnabled = value;
                RaisePropertyChanged(nameof(SingleIsEnabled));
            }
        }

        public bool NotCoveredIsChecked
        {
            get
            {
                return _notCoveredIsChecked;
            }

            set
            {
                _notCoveredIsChecked = value;
                RaisePropertyChanged(nameof(NotCoveredIsChecked));
                if (NotCoveredIsChecked == true)
                {
                    NotCoveredIsEnabled = false;
                    SingleIsChecked = false;
                    SingleIsEnabled = false;
                    RaisePropertyChanged(nameof(NotCoveredIsEnabled));
                    RaisePropertyChanged(nameof(SingleIsChecked));
                    RaisePropertyChanged(nameof(SingleIsEnabled));
                }
                else
                {
                    NotCoveredIsEnabled = true;
                    SingleIsEnabled = true;
                    RaisePropertyChanged(nameof(NotCoveredIsEnabled));
                    RaisePropertyChanged(nameof(SingleIsEnabled));
                }
            }
        }

        public bool NotCoveredIsEnabled
        {
            get
            {
                return _notCoveredIsEnabled;
            }

            set
            {
                _notCoveredIsEnabled = value;
                RaisePropertyChanged(nameof(NotCoveredIsEnabled));
            }
        }

        public int FoalID
        {
            get
            {
                return _foalID;
            }

            set
            {
                _foalID = value;
                RaisePropertyChanged(nameof(FoalID));
            }
        }

        public TribalUse AddedTribalUse
        {
            get
            {
                return _addedTribalUse;
            }

            set
            {
                _addedTribalUse = value;
                RaisePropertyChanged(nameof(AddedTribalUse));
            }
        }

        public ObservableCollection<Horse> FatherHorseList
        {
            get
            {
                return _fatherHorseList;
            }
        }

        #endregion

        #region Commands

        private ICommand _backToHorse;

        public ICommand Back
        {
            get
            {
                if (_backToHorse == null)
                {
                    _backToHorse = new RelayCommand(() =>
                    {
                        _navigationService.NavigateTo("ShowHorsePage", MainHorse);
                    });
                }

                return _backToHorse;
            }

            set
            {
                _backToHorse = value;
            }
        }

        private ICommand _addTribalUseToList;

        public ICommand AddTribalUseToList
        {
            get
            {
                if (_addTribalUseToList == null)
                {
                    AddedTribalUse = new TribalUse();

                    _addTribalUseToList = new RelayCommand(() =>
                    {
                        if (SingleIsChecked == true)
                        {
                            AddedTribalUse.FoalDate = "Холоста";
                        }
                        if (NotCoveredIsChecked == true)
                        {
                            AddedTribalUse.FatherFullName = "Не крыта";
                        }

                        if (SelectedHorse == null)
                        {
                            SelectedHorse = new Horse();
                            SelectedHorse.ID = 0;
                        }
                        if (FatherHorse == null)
                        {
                            FatherHorse = new Horse();
                            FatherHorse.FullName = "Не крыта";
                            FatherHorse.ID = 0;
                        }

                        if (AddedTribalUse.FoalNickName == null)
                        {
                            AddedTribalUse.FoalNickName = string.Empty;
                        }

                        if (AddedTribalUse.FoalNickName.Equals("мертворожденный"))
                        {
                            AddTribalUse();
                        }
                        else if (Horse.AddHorseAsync(AddedTribalUse.FoalNickName, AddedTribalUse.FoalColor, AddedTribalUse.FoalGender, AddedTribalUse.FoalDate, SelectedHorse.ID, FatherHorse.ID).Result)
                        {
                            Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Жеребенок успешно добавлен"));
                            FoalID = Horse.GetLastHorseIDAsync().Result;

                            if (AddedTribalUse.FoalDestination != null)
                            {
                                if (AddedTribalUse.FoalGender == null)
                                {
                                    AddedTribalUse.FoalGender = "Кобыла";
                                }
                                if (Progression.AddProgressionAsync(AddedTribalUse.FoalDate, AddedTribalUse.FoalDestination, string.Empty, FoalID).Result)
                                {
                                    Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Назначение жеребенка успешно добавлено"));
                                    if (AddedTribalUse.FoalDestination.Equals("продажа") || AddedTribalUse.FoalDestination.Equals("списание")
                                    || AddedTribalUse.FoalDestination.Equals("прирезан") || AddedTribalUse.FoalDestination.Equals("обмен"))
                                    {
                                        if (AddedTribalUse.FoalGender.Equals("Жеребец"))
                                        {
                                            Horse.ChangeHorseStateAsync(FoalID, "Выбыл");
                                        }
                                        else
                                        {
                                            Horse.ChangeHorseStateAsync(FoalID, "Выбыла");
                                        }
                                    }
                                    else
                                    {
                                        if (AddedTribalUse.FoalGender.Equals("Жеребец"))
                                        {
                                            Horse.ChangeHorseStateAsync(FoalID, "Действующий");
                                        }
                                        else
                                        {
                                            Horse.ChangeHorseStateAsync(FoalID, "Действующая");
                                        }
                                    }
                                    AddTribalUse();
                                }
                                else
                                {
                                    Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Не удалось добавить назначение жеребенка"));
                                }
                            }
                            else
                            {
                                AddTribalUse();
                            }
                        }
                        else
                        {
                            Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Не удалось добавить жеребенка"));
                            AddTribalUse();
                        }
                    });
                }

                return _addTribalUseToList;
            }

            set
            {
                _addTribalUseToList = value;
            }
        }

        #endregion
    }
}
