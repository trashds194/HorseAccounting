using System.Collections.ObjectModel;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using HorseAccounting.Infra;
using HorseAccounting.Model;

namespace HorseAccounting.ViewModel
{
    public class ShowHorseViewModel : ViewModelBase
    {
        #region Vars

        private IPageNavigationService _navigationService = new PageNavigationService();

        private string _horseNick;

        private Horse _mainHorse;

        // private Horse _motherHorse;
        // private Horse _fatherHorse;
        private ObservableCollection<Horse> _mainHorseList;
        private ObservableCollection<Horse> _motherHorseList;
        private ObservableCollection<Horse> _fatherHorseList;
        private ObservableCollection<Scoring> _mainHorseScoring;

        #endregion

        public ShowHorseViewModel(IPageNavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public void OnPageLoad()
        {
            MainHorse = (Horse)_navigationService.Parameter;
            HorseNick = MainHorse.NickName;
            _mainHorseList = Horse.GetSelectedHorse(MainHorse.ID);
            _motherHorseList = Horse.GetSelectedHorse(MainHorse.MotherID);
            _fatherHorseList = Horse.GetSelectedHorse(MainHorse.FatherID);
            _mainHorseScoring = Scoring.GetSelectedScoring(MainHorse.ID);
            RaisePropertyChanged(() => MainHorseList);
            RaisePropertyChanged(() => MotherHorseList);
            RaisePropertyChanged(() => FatherHorseList);
            RaisePropertyChanged(() => MainHorseScoring);
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

        public ObservableCollection<Horse> MainHorseList
        {
            get
            {
                return _mainHorseList;
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

        public ObservableCollection<Scoring> MainHorseScoring
        {
            get
            {
                return _mainHorseScoring;
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

            set
            {
                _backToHorsesList = value;
            }
        }

        #endregion
    }
}
