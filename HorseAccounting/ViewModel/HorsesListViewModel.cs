using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using HorseAccounting.Infra;
using HorseAccounting.Model;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace HorseAccounting.ViewModel
{
    public class HorsesListViewModel : NavigateViewModel
    {
        private Horse horse;
        private ObservableCollection<Horse> horses;

        public HorsesListViewModel()
        {
            Title = "Главная страница";
            horses = Horse.GetHorses();
            this.RaisePropertyChanged(() => this.HorsesList);          
        }

        private ICommand _addHorse;
        public ICommand AddHorse
        {
            get
            {
                if (_addHorse == null)
                {
                    _addHorse = new RelayCommand(() =>
                    {
                        Navigate("View/AddHorse.xaml");
                    });
                }
                return _addHorse;
            }
            private set { _addHorse = value; }
        }

        public ObservableCollection<Horse> HorsesList
        {
            get
            {
                return horses;
            }
        }

        public Horse SelectedHorse
        {
            get
            {
                return horse;
            }
            set
            {
                horse = value;
                RaisePropertyChanged("SelectedHorse");
            }
        }
    }
}
