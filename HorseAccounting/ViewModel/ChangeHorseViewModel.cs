using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using HorseAccounting.Infra;
using HorseAccounting.Model;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HorseAccounting.ViewModel
{
    public class ChangeHorseViewModel : ViewModelBase
    {
        #region Consts

        private const string _stallionGender = "Жеребец";
        private const string _mareGender = "Кобыла";
        private const string _studFarmName = "К/З им 1КА";

        #endregion

        #region Vars

        private string _studFarm;
        private string _owner;

        private IPageNavigationService _navigationService = new PageNavigationService();

        private Horse _selectedHorse;
        private Horse _mainHorse;
        private Horse _motherHorse;
        private Horse _fatherHorse;

        private string _motherHorseFullName;
        private string _fatherHorseFullName;

        private string _takenBirthPlace;
        private string _takenOwner;

        private ObservableCollection<Horse> _motherHorseList;
        private ObservableCollection<Horse> _fatherHorseList;

        private Gender _gender = Gender.Mare;

        #endregion

        public ChangeHorseViewModel(IPageNavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public void OnPageLoad()
        {
            ComboBoxesUpdate();
            HorseUpdate();
        }

        private void HorseUpdate()
        {
            SelectedHorse = (Horse)_navigationService.Parameter;

            MainHorse = Horse.GetSelectedHorse(SelectedHorse.ID);

            if (MainHorse != null)
            {
                MotherHorse = Horse.GetSelectedHorse(MainHorse.MotherID);
                MotherHorseFullName = MotherHorse.FullName;

                FatherHorse = Horse.GetSelectedHorse(MainHorse.FatherID);
                FatherHorseFullName = FatherHorse.FullName;

                TakenBirthPlace = MainHorse.BirthPlace;
                TakenOwner = MainHorse.Owner;

                if (MainHorse.Gender.Equals("Кобыла"))
                {
                    Gender = Gender.Mare;
                }
                else if (MainHorse.Gender.Equals("Жеребец"))
                {
                    Gender = Gender.Stallion;
                }

                if (MainHorse.BirthPlace.Equals(StudFarmName))
                {
                    StudFarm = StudFarmName;
                    MainHorse.BirthPlace = null;
                }
                else
                {
                    StudFarm = null;
                    MainHorse.BirthPlace = TakenBirthPlace;
                }

                if (MainHorse.Owner.Equals(StudFarmName))
                {
                    Owner = StudFarmName;
                    MainHorse.Owner = null;
                }
                else
                {
                    Owner = null;
                    MainHorse.Owner = TakenOwner;
                }
            }
            else
            {
                Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Не удалось загрузить данные лошади! " +
                    "Проверьте соединение с интернетом или обратитесь к разработчику!"));
            }
        }

        private async void ComboBoxesUpdate()
        {
            await Task.Run(() =>
            {
                _motherHorseList = Horse.GetMotherHorse();
                _fatherHorseList = Horse.GetFatherHorse();
                RaisePropertyChanged(() => MotherHorseList);
                RaisePropertyChanged(() => FatherHorseList);
            }).ConfigureAwait(true);
        }

        private void CheckFields()
        {
            MainHorse = Horse.GetSelectedHorse(SelectedHorse.ID);
            if (MainHorse != null)
            {
                if (!MainHorse.BirthPlace.Equals(StudFarmName))
                {
                    MainHorse.BirthPlace = StudFarm;
                }
                else
                {
                    MainHorse.BirthPlace = null;
                }
                if (!MainHorse.Owner.Equals(StudFarmName))
                {
                    MainHorse.Owner = Owner;
                }
                else
                {
                    MainHorse.Owner = null;
                }
            }
            else
            {
                Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Не удалось загрузить данные лошади! " +
    "Проверьте соединение с интернетом или обратитесь к разработчику!"));
            }

        }

        private void CheckHorseDataForNull()
        {
            if (MainHorse.GpkNum == null)
            {
                MainHorse.GpkNum = string.Empty;
            }
            if (MainHorse.NickName == null)
            {
                MainHorse.NickName = string.Empty;
            }
            if (MainHorse.Brand == null)
            {
                MainHorse.Brand = string.Empty;
            }
            if (MainHorse.Bloodiness == null)
            {
                MainHorse.Bloodiness = string.Empty;
            }
            if (MainHorse.Color == null)
            {
                MainHorse.Color = string.Empty;
            }
            if (MainHorse.BirthPlace == null)
            {
                MainHorse.BirthPlace = string.Empty;
            }
            if (MainHorse.Owner == null)
            {
                MainHorse.Owner = string.Empty;
            }
        }

        #region Definitions

        public string MotherHorseFullName
        {
            get
            {
                return _motherHorseFullName;
            }

            set
            {
                _motherHorseFullName = value;
                if (_motherHorseFullName == null)
                {
                    MotherHorse = null;
                    RaisePropertyChanged(nameof(MotherHorse));
                }
                RaisePropertyChanged(nameof(MotherHorseFullName));
            }
        }

        public string FatherHorseFullName
        {
            get
            {
                return _fatherHorseFullName;
            }

            set
            {
                _fatherHorseFullName = value;
                if (_fatherHorseFullName == null)
                {
                    FatherHorse = null;
                }
                RaisePropertyChanged(nameof(FatherHorseFullName));
            }
        }


        public string TakenBirthPlace
        {
            get
            {
                return _takenBirthPlace;
            }

            set
            {
                _takenBirthPlace = value;
                RaisePropertyChanged(nameof(TakenBirthPlace));
            }
        }

        public string TakenOwner
        {
            get
            {
                return _takenOwner;
            }

            set
            {
                _takenOwner = value;
                RaisePropertyChanged(nameof(TakenOwner));
            }
        }

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

        public Horse SelectedHorse
        {
            get
            {
                return _selectedHorse;
            }

            set
            {
                if (_selectedHorse != value)
                {
                    _selectedHorse = value;
                    RaisePropertyChanged(nameof(SelectedHorse));
                }
            }
        }

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
                MainHorse.BirthPlace = null;
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
                MainHorse.Owner = null;
                RaisePropertyChanged(nameof(Owner));
                RaisePropertyChanged(nameof(IsOwner));
                RaisePropertyChanged(nameof(DropOwner));
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

        private ICommand _changeHorse;

        public ICommand ChangeHorse
        {
            get
            {
                if (_changeHorse == null)
                {
                    MainHorse = new Horse();
                    _changeHorse = new RelayCommand(() =>
                    {
                        CheckHorseDataForNull();

                        if (StudFarm == null)
                        {
                            StudFarm = MainHorse.BirthPlace;
                        }

                        if (Owner == null)
                        {
                            Owner = MainHorse.Owner;
                        }

                        if (MotherHorse != null && FatherHorse != null)
                        {
                            if (Horse.ChangeHorse(MainHorse.ID, MainHorse.GpkNum, MainHorse.NickName, MainHorse.Brand, MainHorse.Bloodiness, MainHorse.Color, GetGenderResult, MainHorse.BirthDate, StudFarm, Owner, MotherHorse.ID, FatherHorse.ID))
                            {
                                Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Вы успешно обновили запись лошади"));
                                CheckFields();
                            }
                            else
                            {
                                Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Не удалось обновить запись лошади"));
                            }
                        }
                        else if (MotherHorse != null && FatherHorse == null)
                        {
                            if (Horse.ChangeHorse(MainHorse.ID, MainHorse.GpkNum, MainHorse.NickName, MainHorse.Brand, MainHorse.Bloodiness, MainHorse.Color, GetGenderResult, MainHorse.BirthDate, StudFarm, Owner, MotherHorse.ID, 0))
                            {
                                Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Вы успешно обновили запись лошади"));
                                CheckFields();
                            }
                            else
                            {
                                Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Не удалось обновить запись лошади"));
                            }
                        }
                        else if (MotherHorse == null && FatherHorse != null)
                        {
                            if (Horse.ChangeHorse(MainHorse.ID, MainHorse.GpkNum, MainHorse.NickName, MainHorse.Brand, MainHorse.Bloodiness, MainHorse.Color, GetGenderResult, MainHorse.BirthDate, StudFarm, Owner, 0, FatherHorse.ID))
                            {
                                Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Вы успешно обновили запись лошади"));
                                CheckFields();
                            }
                            else
                            {
                                Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Не удалось обновить запись лошади"));
                            }
                        }
                        else
                        {
                            if (Horse.ChangeHorse(MainHorse.ID, MainHorse.GpkNum, MainHorse.NickName, MainHorse.Brand, MainHorse.Bloodiness, MainHorse.Color, GetGenderResult, MainHorse.BirthDate, StudFarm, Owner, 0, 0))
                            {
                                Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Вы успешно обновили запись лошади"));
                                CheckFields();
                            }
                            else
                            {
                                Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Не удалось обновили запись лошади"));
                            }
                        }
                    });
                }

                return _changeHorse;
            }

            set
            {
                _changeHorse = value;
            }
        }

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
