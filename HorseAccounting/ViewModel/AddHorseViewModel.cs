using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using HorseAccounting.Infra;
using HorseAccounting.Model;
using System.Windows.Input;

namespace HorseAccounting.ViewModel
{
    public class AddHorseViewModel : NavigateViewModel
    {
        private Horse horse;   

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
                        if(Horse.AddHorse(AddedHorse.GpkNum, AddedHorse.NickName, AddedHorse.Brand, AddedHorse.Bloodiness, AddedHorse.Color,
                            AddedHorse.BirthDate, AddedHorse.BirthPlace, AddedHorse.Owner))
                        {
                            Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Вы успешно добавили лошадь"));
                        }
                        
                    });
                }
                return _addHorse;
            }
            set { _addHorse = value; }
        }
    }
}
