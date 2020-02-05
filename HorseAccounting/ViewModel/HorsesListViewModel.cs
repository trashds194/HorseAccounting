using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using HorseAccounting.Infra;
using HorseAccounting.Model;
using HorseAccounting.View;

namespace HorseAccounting.ViewModel
{
    public class HorsesListViewModel : ViewModelBase
    {
        #region Vars

        private IPageNavigationService _navigationService;
        private Horse _horse;
        private ObservableCollection<Horse> _horses;
        private ShowHorseViewModel _showHorse;

        #endregion

        public HorsesListViewModel(IPageNavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public void OnPageLoad()
        {
            _horses = Horse.GetHorses();
            RaisePropertyChanged(() => HorsesList);
        }

        #region Definitions

        public ObservableCollection<Horse> HorsesList
        {
            get
            {
                return _horses;
            }
        }

        public Horse SelectedHorse
        {
            get
            {
                return _horse;
            }

            set
            {
                _horse = value;
                RaisePropertyChanged(nameof(SelectedHorse));
            }
        }

        public ShowHorseViewModel ShowHorse
        {
            get
            {
                return _showHorse;
            }

            set
            {
                if (_showHorse != null)
                {
                    _showHorse = value;
                    RaisePropertyChanged(nameof(ShowHorse));
                }
            }
        }

        #endregion

        #region Commands

        private RelayCommand _addHorse;

        public RelayCommand AddHorse
        {
            get
            {
                return _addHorse
                    ?? (_addHorse = new RelayCommand(
                    () =>
                    {
                        _navigationService.NavigateTo("AddHorsePage");
                    }));
            }

            private set
            {
                _addHorse = value;
            }
        }

        #endregion

        public void DoubleClickMethod()
        {
            if (SelectedHorse != null)
            {
                if (SelectedHorse.ID != 0)
                {
                    _navigationService.NavigateTo("ShowHorsePage", SelectedHorse);
                }
            }
        }
    }
}
