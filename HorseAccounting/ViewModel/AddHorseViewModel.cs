using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using HorseAccounting.Infra;
using HorseAccounting.Model;
using System.Collections.ObjectModel;
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

        public void OnPageLoad()
        {
            ComboBoxesUpdate();
        }

        private void ComboBoxesUpdate()
        {
            _motherHorseList = Horse.GetMotherHorse();
            _fatherHorseList = Horse.GetFatherHorse();
            RaisePropertyChanged(() => MotherHorseList);
            RaisePropertyChanged(() => FatherHorseList);
        }

        private void CheckHorseDataForNull()
        {
            if (AddedHorse.GpkNum == null)
            {
                AddedHorse.GpkNum = string.Empty;
            }
            if (AddedHorse.NickName == null)
            {
                AddedHorse.NickName = string.Empty;
            }
            if (AddedHorse.Brand == null)
            {
                AddedHorse.Brand = string.Empty;
            }
            if (AddedHorse.Bloodiness == null)
            {
                AddedHorse.Bloodiness = string.Empty;
            }
            if (AddedHorse.Color == null)
            {
                AddedHorse.Color = string.Empty;
            }
            if (AddedHorse.BirthPlace == null)
            {
                AddedHorse.BirthPlace = string.Empty;
            }
            if (AddedHorse.Owner == null)
            {
                AddedHorse.Owner = string.Empty;
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

                        if (StudFarm == null)
                        {
                            StudFarm = AddedHorse.BirthPlace;
                        }

                        if (Owner == null)
                        {
                            Owner = AddedHorse.Owner;
                        }

                        if (MotherHorse != null && FatherHorse != null)
                        {
                            if (Horse.AddHorse(AddedHorse.GpkNum, AddedHorse.NickName, AddedHorse.Brand, AddedHorse.Bloodiness, AddedHorse.Color, GetGenderResult, AddedHorse.BirthDate, StudFarm, Owner, MotherHorse.ID, FatherHorse.ID, AddedState))
                            {
                                Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Вы успешно добавили запись лошади"));
                                LastHorseID = Horse.GetLastHorseID();
                                if (LastHorseID != 0)
                                {
                                    if (AddedProgression.Comment == null)
                                    {
                                        AddedProgression.Comment = string.Empty;
                                    }
                                    if (!string.IsNullOrEmpty(AddedProgression.Date))
                                    {
                                        if (Progression.AddProgression(AddedProgression.Date, AddedProgression.Destination, AddedProgression.Comment, LastHorseID))
                                        {
                                            Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Вы успешно добавили движение лошади"));
                                            if (AddedProgression.Destination.Equals("продажа") || AddedProgression.Destination.Equals("списание"))
                                            {
                                                if (GetGenderResult.Equals(StallionGender))
                                                {
                                                    Horse.ChangeHorseState(LastHorseID, "Выбыл");
                                                }
                                                else
                                                {
                                                    Horse.ChangeHorseState(LastHorseID, "Выбыла");
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
                        }
                        else if (MotherHorse != null && FatherHorse == null)
                        {
                            if (Horse.AddHorse(AddedHorse.GpkNum, AddedHorse.NickName, AddedHorse.Brand, AddedHorse.Bloodiness, AddedHorse.Color, GetGenderResult, AddedHorse.BirthDate, StudFarm, Owner, MotherHorse.ID, 0, AddedState))
                            {
                                Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Вы успешно добавили запись лошади"));
                                LastHorseID = Horse.GetLastHorseID();
                                if (LastHorseID != 0)
                                {
                                    if (AddedProgression.Comment == null)
                                    {
                                        AddedProgression.Comment = string.Empty;
                                    }
                                    if (!string.IsNullOrEmpty(AddedProgression.Date))
                                    {
                                        if (Progression.AddProgression(AddedProgression.Date, AddedProgression.Destination, AddedProgression.Comment, LastHorseID))
                                        {
                                            Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Вы успешно добавили движение лошади"));
                                            if (AddedProgression.Destination.Equals("продажа") || AddedProgression.Destination.Equals("списание"))
                                            {
                                                if (GetGenderResult.Equals(StallionGender))
                                                {
                                                    Horse.ChangeHorseState(LastHorseID, "Выбыл");
                                                }
                                                else
                                                {
                                                    Horse.ChangeHorseState(LastHorseID, "Выбыла");
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
                        }
                        else if (MotherHorse == null && FatherHorse != null)
                        {
                            if (Horse.AddHorse(AddedHorse.GpkNum, AddedHorse.NickName, AddedHorse.Brand, AddedHorse.Bloodiness, AddedHorse.Color, GetGenderResult, AddedHorse.BirthDate, StudFarm, Owner, 0, FatherHorse.ID, AddedState))
                            {
                                Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Вы успешно добавили запись лошади"));
                                LastHorseID = Horse.GetLastHorseID();
                                if (LastHorseID != 0)
                                {
                                    if (AddedProgression.Comment == null)
                                    {
                                        AddedProgression.Comment = string.Empty;
                                    }
                                    if (!string.IsNullOrEmpty(AddedProgression.Date))
                                    {
                                        if (Progression.AddProgression(AddedProgression.Date, AddedProgression.Destination, AddedProgression.Comment, LastHorseID))
                                        {
                                            Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Вы успешно добавили движение лошади"));
                                            if (AddedProgression.Destination.Equals("продажа") || AddedProgression.Destination.Equals("списание"))
                                            {
                                                if (GetGenderResult.Equals(StallionGender))
                                                {
                                                    Horse.ChangeHorseState(LastHorseID, "Выбыл");
                                                }
                                                else
                                                {
                                                    Horse.ChangeHorseState(LastHorseID, "Выбыла");
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
                        }
                        else
                        {
                            if (Horse.AddHorse(AddedHorse.GpkNum, AddedHorse.NickName, AddedHorse.Brand, AddedHorse.Bloodiness, AddedHorse.Color, GetGenderResult, AddedHorse.BirthDate, StudFarm, Owner, 0, 0, AddedState))
                            {
                                Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Вы успешно добавили запись лошади"));
                                LastHorseID = Horse.GetLastHorseID();
                                if (LastHorseID != 0)
                                {
                                    if (AddedProgression.Comment == null)
                                    {
                                        AddedProgression.Comment = string.Empty;
                                    }
                                    if (!string.IsNullOrEmpty(AddedProgression.Date))
                                    {
                                        if (Progression.AddProgression(AddedProgression.Date, AddedProgression.Destination, AddedProgression.Comment, LastHorseID))
                                        {
                                            Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Вы успешно добавили движение лошади"));
                                            if (AddedProgression.Destination.Equals("продажа") || AddedProgression.Destination.Equals("списание"))
                                            {
                                                if (GetGenderResult.Equals(StallionGender))
                                                {
                                                    Horse.ChangeHorseState(LastHorseID, "Выбыл");
                                                }
                                                else
                                                {
                                                    Horse.ChangeHorseState(LastHorseID, "Выбыла");
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
