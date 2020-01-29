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

    public class AddHorseViewModel : NavigateViewModel
    {
        #region Consts

        private const string stallionGender = "Жеребец";
        private const string mareGender = "Кобыла";
        private const string studFarmName = "К/З им 1КА";

        #endregion

        private string studFarm;
        private string owner;
        private Horse horse;
        Gender gender = Gender.Mare;

        public AddHorseViewModel()
        {
            Title = "Добавление лошади";
        }

        #region Definition

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
                    return;

                gender = value;
                RaisePropertyChanged(nameof(Gender));
                RaisePropertyChanged(nameof(IsStallion));
                RaisePropertyChanged(nameof(IsMare));
                RaisePropertyChanged(nameof(GetResult));
            }
        }

        public static string StudFarmName => studFarmName;
        public static string StallionGender => stallionGender;
        public static string MareGender => mareGender;

        public string StudFarm
        {
            get
            {
                return studFarm;
            }
            set
            {
                if (studFarm == value)
                {
                    return;
                }

                studFarm = value;
                RaisePropertyChanged(nameof(StudFarm));
            }
        }

        public string Owner
        {
            get
            {
                return owner;
            }
            set
            {
                if (owner == value)
                {
                    return;
                }

                owner = value;
                RaisePropertyChanged(nameof(Owner));
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
                        Navigate("View/HorsesList.xaml");
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
                        if (Horse.AddHorse(AddedHorse.GpkNum, AddedHorse.NickName, AddedHorse.Brand, AddedHorse.Bloodiness, AddedHorse.Color, GetResult,
                            AddedHorse.BirthDate, AddedHorse.BirthPlace, AddedHorse.Owner))
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

        #endregion
    }
}
