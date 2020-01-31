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
        #region Vars

        private Horse horse;
        private ObservableCollection<Horse> horses;

        #endregion

        public HorsesListViewModel()
        {
            Title = "Главная страница";
            horses = Horse.GetHorses();
            this.RaisePropertyChanged(() => this.HorsesList);
        }

        #region Definitions

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
                RaisePropertyChanged(nameof(SelectedHorse));
            }
        }

        #endregion

        #region Commands

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

        #endregion

        public void DoubleClickMethod()
        {
            if (SelectedHorse != null)
            {
                if(SelectedHorse.ID != 0)
                {
                    Horse.ReceivedID = SelectedHorse.ID;
                    Navigate("View/ShowHorse.xaml");
                }
            }
        }
    }
}
