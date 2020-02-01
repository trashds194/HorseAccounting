using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using HorseAccounting.Infra;
using HorseAccounting.Model;
using System.Windows.Input;

namespace HorseAccounting.ViewModel
{
    public enum Gender
    {
        Stallion,
        Mare
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
        private Horse horse;
        Gender gender = Gender.Mare;

        #endregion

        public AddHorseViewModel(IPageNavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        #region Definitions

        public Horse AddedHorse
        {
            get
            {
                return horse;
            }
            set
            {
                horse = value;
                RaisePropertyChanged(nameof(AddedHorse));
            }
        }

        public Gender Gender
        {
            get { return gender; }
            set
            {
                if (gender == value)
                {
                    return;
                }

                gender = value;
                RaisePropertyChanged(nameof(Gender));
                RaisePropertyChanged(nameof(IsStallion));
                RaisePropertyChanged(nameof(IsMare));
                RaisePropertyChanged(nameof(GetResult));
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
                RaisePropertyChanged(nameof(Owner));
                RaisePropertyChanged(nameof(IsOwner));
                RaisePropertyChanged(nameof(DropOwner));
            }
        }

        #endregion

        #region RadioButtons

        public bool IsStallion
        {
            get { return Gender == Gender.Stallion; }
            set
            {
                Gender = value ? Gender.Stallion : Gender;
                Messenger.Default.Send<NotificationMessage>(new NotificationMessage(GetResult));
            }
        }

        public bool IsMare
        {
            get { return Gender == Gender.Mare; }
            set
            {
                Gender = value ? Gender.Mare : Gender;
                Messenger.Default.Send<NotificationMessage>(new NotificationMessage(GetResult));
            }
        }

        public bool IsStudFarm
        {
            get { return StudFarm == StudFarmName; }
            set
            {
                StudFarm = value ? StudFarmName : StudFarm;
                Messenger.Default.Send<NotificationMessage>(new NotificationMessage(StudFarm));
            }
        }

        public bool IsOwner
        {
            get { return Owner == StudFarmName; }
            set
            {
                Owner = value ? StudFarmName : Owner;
                Messenger.Default.Send<NotificationMessage>(new NotificationMessage(Owner));
            }
        }

        public string GetResult
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
                        _navigationService.NavigateTo("Home");
                    });
                }
                return _horsesList;
            }
            set { _horsesList = value; }
        }

        private ICommand _addHorse;
        public ICommand AddHorseToList
        {
            get
            {
                if (_addHorse == null)
                {
                    AddedHorse = new Horse();
                    _addHorse = new RelayCommand(() =>
                    {
                        if (StudFarm == null)
                        {
                            StudFarm = AddedHorse.BirthPlace;
                        }
                        if (Owner == null)
                        {
                            Owner = AddedHorse.Owner;
                        }
                        if (Horse.AddHorse(AddedHorse.GpkNum, AddedHorse.NickName, AddedHorse.Brand, AddedHorse.Bloodiness, AddedHorse.Color, GetResult,
                            AddedHorse.BirthDate, StudFarm, Owner))
                        {
                            Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Вы успешно добавили запись лошадь"));
                            AddedHorse.CleanHorseData();
                        }
                        else
                        {
                            Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Не удалось добавить запись лошади"));
                        }
                    });
                }
                return _addHorse;
            }
            set { _addHorse = value; }
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
            set { _dropStudFarm = value; }
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
            set { _dropOwner = value; }
        }

        #endregion
    }
}
