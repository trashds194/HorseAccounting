using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using HorseAccounting.Infra;
using HorseAccounting.Model;
using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HorseAccounting.ViewModel
{
    public enum Gender
    {
        Stallion,
        Mare,
    }

    public class AddHorseViewModel : ViewModelBase
    {
        #region Consts

        private const string _stallionGender = "Жеребец";
        private const string _mareGender = "Кобыла";
        private const string _studFarmName = "К/З им 1КА";

        #endregion

        #region Vars

        private IPageNavigationService _navigationService;

        private string _studFarm;
        private string _owner;
        private string _addedState = "Действующая";
        private string _chipCountry;
        private string _fullChip;

        private int _lastHorseID;

        private ObservableCollection<Horse> _motherHorseList;
        private ObservableCollection<Horse> _fatherHorseList;

        private Horse _addedHorse;
        private Horse _motherHorse;
        private Horse _fatherHorse;

        private Progression _addedProgression;

        private Gender _gender = Gender.Mare;

        #endregion

        public AddHorseViewModel(IPageNavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public async void OnPageLoad()
        {
            await Task.Run(() =>
            {
                ComboBoxesUpdate();
            }).ConfigureAwait(true);
        }

        private async void ComboBoxesUpdate()
        {
            await Task.Run(() =>
            {
                try
                {
                    _motherHorseList = Horse.GetMotherHorseAsync().Result;
                    _fatherHorseList = Horse.GetFatherHorseAsync().Result;
                    RaisePropertyChanged(() => MotherHorseList);
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

        private void CheckHorseDataForNull()
        {
            if (MotherHorse == null)
            {
                MotherHorse = new Horse();
                MotherHorse.ID = 0;
            }

            if (FatherHorse == null)
            {
                FatherHorse = new Horse();
                FatherHorse.ID = 0;
            }

            if (StudFarm == null)
            {
                StudFarm = AddedHorse.BirthPlace;
            }

            if (Owner == null)
            {
                Owner = AddedHorse.Owner;
            }
        }

        #region Definitions

        public ObservableCollection<Horse> MotherHorseList
        {
            get
            {
                return _motherHorseList;
            }
        }

        public ObservableCollection<Horse> FatherHorseList
        {
            get
            {
                return _fatherHorseList;
            }
        }

        public Horse AddedHorse
        {
            get
            {
                return _addedHorse;
            }

            set
            {
                _addedHorse = value;
                RaisePropertyChanged(nameof(AddedHorse));
            }
        }

        public Horse MotherHorse
        {
            get
            {
                return _motherHorse;
            }

            set
            {
                _motherHorse = value;
                RaisePropertyChanged(nameof(MotherHorse));
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

        public Progression AddedProgression
        {
            get
            {
                return _addedProgression;
            }

            set
            {
                _addedProgression = value;
                RaisePropertyChanged(nameof(AddedProgression));
            }
        }

        public Gender Gender
        {
            get
            {
                return _gender;
            }

            set
            {
                if (_gender == value)
                {
                    return;
                }

                _gender = value;
                RaisePropertyChanged(nameof(Gender));
                RaisePropertyChanged(nameof(IsStallion));
                RaisePropertyChanged(nameof(IsMare));
                RaisePropertyChanged(nameof(GetGenderResult));
            }
        }

        public static string StudFarmName => _studFarmName;

        public static string StallionGender => _stallionGender;

        public static string MareGender => _mareGender;

        public string AddedState
        {
            get
            {
                return _addedState;
            }

            set
            {
                _addedState = value;
                RaisePropertyChanged(nameof(AddedState));
            }
        }

        public string ChipCountry
        {
            get
            {
                return _chipCountry;
            }

            set
            {
                _chipCountry = value;
                RaisePropertyChanged(nameof(ChipCountry));
            }
        }

        public string FullChip
        {
            get
            {
                return _fullChip;
            }

            set
            {
                _fullChip = value;
                RaisePropertyChanged(nameof(FullChip));
            }
        }

        public string StudFarm
        {
            get
            {
                return _studFarm;
            }

            set
            {
                if (_studFarm == value)
                {
                    return;
                }

                _studFarm = value;
                AddedHorse.BirthPlace = null;
                RaisePropertyChanged(nameof(StudFarm));
                RaisePropertyChanged(nameof(IsStudFarm));
                RaisePropertyChanged(nameof(DropStudFarm));
            }
        }

        public string Owner
        {
            get
            {
                return _owner;
            }

            set
            {
                if (_owner == value)
                {
                    return;
                }

                _owner = value;
                AddedHorse.Owner = null;
                RaisePropertyChanged(nameof(Owner));
                RaisePropertyChanged(nameof(IsOwner));
                RaisePropertyChanged(nameof(DropOwner));
            }
        }

        public int LastHorseID
        {
            get
            {
                return _lastHorseID;
            }

            set
            {
                _lastHorseID = value;
                RaisePropertyChanged(nameof(LastHorseID));
            }
        }

        #endregion

        #region RadioButtons

        public bool IsStallion
        {
            get
            {
                return Gender == Gender.Stallion;
            }

            set
            {
                Gender = value ? Gender.Stallion : Gender;
                AddedState = "Действующий";
            }
        }

        public bool IsMare
        {
            get
            {
                return Gender == Gender.Mare;
            }

            set
            {
                Gender = value ? Gender.Mare : Gender;
                AddedState = "Действующая";
            }
        }

        public bool IsStudFarm
        {
            get
            {
                return StudFarm == StudFarmName;
            }

            set
            {
                StudFarm = value ? StudFarmName : StudFarm;
            }
        }

        public bool IsOwner
        {
            get
            {
                return Owner == StudFarmName;
            }

            set
            {
                Owner = value ? StudFarmName : Owner;
            }
        }

        public string GetGenderResult
        {
            get
            {
                switch (Gender)
                {
                    case Gender.Stallion:
                        return StallionGender;
                    case Gender.Mare:
                        return MareGender;
                }

                return null;
            }
        }

        #endregion

        #region Commands

        private ICommand _horsesList;

        public ICommand BackToList
        {
            get
            {
                if (_horsesList == null)
                {
                    _horsesList = new RelayCommand(() =>
                    {
                        _navigationService.NavigateTo("HorsesList");
                    });
                }

                return _horsesList;
            }

            set
            {
                _horsesList = value;
            }
        }

        private ICommand _addHorse;

        public ICommand AddHorseToList
        {
            get
            {
                if (_addHorse == null)
                {
                    AddedHorse = new Horse();
                    AddedProgression = new Progression();
                    _addHorse = new RelayCommand(() =>
                    {
                        CheckHorseDataForNull();

                        if (!string.IsNullOrEmpty(AddedHorse.ChipNumber))
                        {
                            FullChip = AddedHorse.ChipNumber + " " + ChipCountry;
                        }
                        else
                        {
                            FullChip = string.Empty;
                        }

                        if (Horse.AddHorseAsync(AddedHorse.GpkNum, AddedHorse.NickName, AddedHorse.Brand, AddedHorse.Bloodiness, AddedHorse.Color, AddedHorse.Breed, 
                            AddedHorse.TheClass, FullChip, GetGenderResult, AddedHorse.BirthDate, StudFarm, Owner, MotherHorse.ID, FatherHorse.ID, AddedState).Result)
                        {
                            Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Вы успешно добавили запись лошади"));
                            LastHorseID = Horse.GetLastHorseIDAsync().Result;
                            if (LastHorseID != 0)
                            {
                                if (!string.IsNullOrEmpty(AddedProgression.Date))
                                {
                                    if (Progression.AddProgressionAsync(AddedProgression.Date, AddedProgression.Destination, AddedProgression.Comment, LastHorseID).Result)
                                    {
                                        Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Вы успешно добавили движение лошади"));
                                        if (AddedProgression.Destination.Equals("продажа") || AddedProgression.Destination.Equals("списание")
                                        || AddedProgression.Destination.Equals("прирезан") || AddedProgression.Destination.Equals("обмен"))
                                        {
                                            if (GetGenderResult.Equals(StallionGender))
                                            {
                                                Horse.ChangeHorseStateAsync(LastHorseID, "Выбыл");
                                            }
                                            else
                                            {
                                                Horse.ChangeHorseStateAsync(LastHorseID, "Выбыла");
                                            }
                                        }
                                        AddedProgression.CleanProgressionData();
                                    }
                                    else
                                    {
                                        Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Ошибка при добавлении движения, проверьте корректность введенных данных!"));
                                    }
                                }
                            }
                            else
                            {
                                Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Не удалось добавить движение, обратитесь к разработчику!"));
                            }
                            AddedHorse.CleanHorseData();
                            ComboBoxesUpdate();
                        }
                        else
                        {
                            Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Не удалось добавить запись лошади, проверьте корректность введенных данных!"));
                        }
                    });
                }

                return _addHorse;
            }

            set
            {
                _addHorse = value;
            }
        }

        private ICommand _dropStudFarm;

        public ICommand DropStudFarm
        {
            get
            {
                if (_dropStudFarm == null)
                {
                    _dropStudFarm = new RelayCommand(() =>
                    {
                        StudFarm = null;
                    });
                }

                return _dropStudFarm;
            }

            set
            {
                _dropStudFarm = value;
            }
        }

        private ICommand _dropOwner;

        public ICommand DropOwner
        {
            get
            {
                if (_dropOwner == null)
                {
                    _dropOwner = new RelayCommand(() =>
                    {
                        Owner = null;
                    });
                }

                return _dropOwner;
            }

            set
            {
                _dropOwner = value;
            }
        }

        #endregion
    }
}
