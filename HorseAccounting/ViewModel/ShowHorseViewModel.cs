using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using HorseAccounting.Infra;
using HorseAccounting.Model;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace HorseAccounting.ViewModel
{
    public class ShowHorseViewModel : ViewModelBase
    {
        #region Vars

        private IPageNavigationService _navigationService = new PageNavigationService();
        private string _horseNick;
        private Horse _selectedHorse;
        private ObservableCollection<Horse> _selectedHorseList;

        #endregion

        public ShowHorseViewModel(IPageNavigationService navigationService)
        {
            //Title = "Просмотр лошади";
            _navigationService = navigationService;

        }

        public void OnPageLoad()
        {
            this.SelectedHorse = (Horse)_navigationService.Parameter;
            HorseNick = SelectedHorse.NickName;
            _selectedHorseList = Horse.GetSelectedHorse(SelectedHorse.ID);
            this.RaisePropertyChanged(() => this.SelectedHorseList);
        }

        #region Definitions

        public string HorseNick
        {
            get
            {
                return _horseNick;
            }
            set
            {
                _horseNick = value;
                RaisePropertyChanged(nameof(HorseNick));
            }
        }

        public ObservableCollection<Horse> SelectedHorseList
        {
            get
            {
                return _selectedHorseList;
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

        #endregion

        #region Commands

        private ICommand _backToHorsesList;
        public ICommand BackToList
        {
            get
            {
                if (_backToHorsesList == null)
                {
                    _backToHorsesList = new RelayCommand(() =>
                    {
                        _navigationService.NavigateTo("Home");
                    });
                }
                return _backToHorsesList;
            }
            set { _backToHorsesList = value; }
        }

        #endregion
    }
}
