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
        private Horse horse;
        Gender gender = Gender.Mare;

        public AddHorseViewModel()
        {
            Title = "Добавление лошади";
        }

        public Horse AddedHorse
        {
            get
            {
                return horse;
            }
            set
            {
                horse = value;
                RaisePropertyChanged("AddedHorse");
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
                RaisePropertyChanged("Gender");
                RaisePropertyChanged("IsStallion");
                RaisePropertyChanged("IsMare");
                RaisePropertyChanged("GetResult");
            }
        }

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

        public string GetResult
        {
            get
            {
                switch (Gender)
                {
                    case Gender.Stallion:
                        return "Жеребец";
                    case Gender.Mare:
                        return "Кобыла";
                }
                return "";
            }
        }

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
