using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using HorseAccounting.Infra;
using HorseAccounting.Model;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HorseAccounting.ViewModel
{
    public class TaleMagazineViewModel : ViewModelBase
    {
        #region Vars

        private IPageNavigationService _navigationService = new PageNavigationService();

        private Horse _stallionHorse;
        private Horse _mare1Horse;
        private Horse _mare2Horse;
        private Horse _mare3Horse;
        private Horse _mare4Horse;
        private Horse _mare5Horse;
        private Horse _mare6Horse;
        private Horse _mare7Horse;
        private Horse _mare8Horse;
        private Horse _mare9Horse;
        private Horse _mare10Horse;

        private ObservableCollection<Horse> _stallionHorseList;
        private ObservableCollection<Horse> _mareHorseList;

        #endregion

        public TaleMagazineViewModel(IPageNavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public async void OnPageLoad()
        {
            await Task.Run(() =>
            {

                _stallionHorseList = Horse.GetFatherHorse();
                _mareHorseList = Horse.GetMotherHorse();

                RaisePropertyChanged(() => StallionHorseList);
                RaisePropertyChanged(() => MareHorseList);

            }).ConfigureAwait(true);
        }

        #region Definitions

        public Horse StallionHorse
        {
            get
            {
                return _stallionHorse;
            }

            set
            {
                if (_stallionHorse != value)
                {
                    _stallionHorse = value;
                    RaisePropertyChanged(nameof(StallionHorse));
                }
            }
        }

        public Horse Mare1Horse
        {
            get
            {
                return _mare1Horse;
            }

            set
            {
                if (_mare1Horse != value)
                {
                    _mare1Horse = value;
                    RaisePropertyChanged(nameof(Mare1Horse));
                }
            }
        }

        public Horse Mare2Horse
        {
            get
            {
                return _mare2Horse;
            }

            set
            {
                if (_mare2Horse != value)
                {
                    _mare2Horse = value;
                    RaisePropertyChanged(nameof(Mare2Horse));
                }
            }
        }

        public Horse Mare3Horse
        {
            get
            {
                return _mare3Horse;
            }

            set
            {
                if (_mare3Horse != value)
                {
                    _mare3Horse = value;
                    RaisePropertyChanged(nameof(Mare3Horse));
                }
            }
        }

        public Horse Mare4Horse
        {
            get
            {
                return _mare4Horse;
            }

            set
            {
                if (_mare4Horse != value)
                {
                    _mare4Horse = value;
                    RaisePropertyChanged(nameof(Mare4Horse));
                }
            }
        }

        public Horse Mare5Horse
        {
            get
            {
                return _mare5Horse;
            }

            set
            {
                if (_mare5Horse != value)
                {
                    _mare5Horse = value;
                    RaisePropertyChanged(nameof(Mare5Horse));
                }
            }
        }

        public Horse Mare6Horse
        {
            get
            {
                return _mare6Horse;
            }

            set
            {
                if (_mare6Horse != value)
                {
                    _mare6Horse = value;
                    RaisePropertyChanged(nameof(Mare6Horse));
                }
            }
        }

        public Horse Mare7Horse
        {
            get
            {
                return _mare7Horse;
            }

            set
            {
                if (_mare7Horse != value)
                {
                    _mare7Horse = value;
                    RaisePropertyChanged(nameof(Mare7Horse));
                }
            }
        }

        public Horse Mare8Horse
        {
            get
            {
                return _mare8Horse;
            }

            set
            {
                if (_mare8Horse != value)
                {
                    _mare8Horse = value;
                    RaisePropertyChanged(nameof(Mare8Horse));
                }
            }
        }

        public Horse Mare9Horse
        {
            get
            {
                return _mare9Horse;
            }

            set
            {
                if (_mare9Horse != value)
                {
                    _mare9Horse = value;
                    RaisePropertyChanged(nameof(Mare9Horse));
                }
            }
        }

        public Horse Mare10Horse
        {
            get
            {
                return _mare10Horse;
            }

            set
            {
                if (_mare10Horse != value)
                {
                    _mare10Horse = value;
                    RaisePropertyChanged(nameof(Mare10Horse));
                }
            }
        }

        public ObservableCollection<Horse> StallionHorseList
        {
            get
            {
                return _stallionHorseList;
            }
        }

        public ObservableCollection<Horse> MareHorseList
        {
            get
            {
                return _mareHorseList;
            }
        }

        #endregion

        #region WindowCommands

        private ICommand _backToHorse;

        public ICommand Back
        {
            get
            {
                if (_backToHorse == null)
                {
                    _backToHorse = new RelayCommand(() =>
                    {
                        _navigationService.NavigateTo("HorsesList");
                    });
                }

                return _backToHorse;
            }

            set
            {
                _backToHorse = value;
            }
        }

        #endregion
    }
}
