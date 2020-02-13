using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using HorseAccounting.Infra;
using HorseAccounting.Model;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HorseAccounting.ViewModel
{
    public class ShowHorseViewModel : ViewModelBase
    {
        #region Vars

        private IPageNavigationService _navigationService = new PageNavigationService();

        private string _horseNick;

        private Horse _mainHorse;
        private Horse _selectedHorse;
        private Horse _motherHorse;
        private Horse _fatherHorse;

        private ObservableCollection<Scoring> _mainHorseScoring;

        #endregion

        public ShowHorseViewModel(IPageNavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public async void OnPageLoad()
        {
            await Task.Run(() =>
            {
                MainHorse = (Horse)_navigationService.Parameter;

                HorseNick = MainHorse.FullName;

                SelectedHorse = Horse.GetSelectedHorse(MainHorse.ID);
                MotherHorse = Horse.GetSelectedHorse(SelectedHorse.MotherID);
                FatherHorse = Horse.GetSelectedHorse(SelectedHorse.FatherID);
                _mainHorseScoring = Scoring.GetSelectedScoring(MainHorse.ID);

                RaisePropertyChanged(() => MainHorseScoring);
            }).ConfigureAwait(true);
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

        public Horse MotherHorse
        {
            get
            {
                return _motherHorse;
            }

            set
            {
                if (_motherHorse != value)
                {
                    _motherHorse = value;
                    RaisePropertyChanged(nameof(MotherHorse));
                }
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
                if (_fatherHorse != value)
                {
                    _fatherHorse = value;
                    RaisePropertyChanged(nameof(FatherHorse));
                }
            }
        }

        #endregion

        #region MenuCommands

        #endregion

        #region WindowCommands

        private ICommand _backToHorsesList;

        public ICommand BackToList
        {
            get
            {
                if (_backToHorsesList == null)
                {
                    _backToHorsesList = new RelayCommand(() =>
                    {
                        MainHorse = null;
                        SelectedHorse = null;
                        MotherHorse = null;
                        FatherHorse = null;
                        _navigationService.NavigateTo("HorsesList");
                    });
                }

                return _backToHorsesList;
            }

            set
            {
                _backToHorsesList = value;
            }
        }

        private ICommand _changeHorse;

        public ICommand ChangeHorse
        {
            get
            {
                if (_changeHorse == null)
                {
                    _changeHorse = new RelayCommand(() =>
                    {
                        _navigationService.NavigateTo("ChangeHorsePage", MainHorse);
                    });
                }

                return _changeHorse;
            }

            set
            {
                _changeHorse = value;
            }
        }

        private ICommand _addScoring;

        public ICommand AddScoring
        {
            get
            {
                if (_addScoring == null)
                {
                    _addScoring = new RelayCommand(() =>
                    {
                        _navigationService.NavigateTo("AddScoringPage", MainHorse);
                    });
                }

                return _addScoring;
            }

            set
            {
                _addScoring = value;
            }
        }

        #endregion
    }
}
