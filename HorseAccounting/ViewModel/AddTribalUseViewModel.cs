using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using HorseAccounting.Infra;
using HorseAccounting.Model;
using System.Windows.Input;

namespace HorseAccounting.ViewModel
{
    public class AddTribalUseViewModel : ViewModelBase
    {
        #region Vars

        private IPageNavigationService _navigationService = new PageNavigationService();

        private Horse _mainHorse;

        #endregion

        public AddTribalUseViewModel(IPageNavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public void OnPageLoad()
        {
            MainHorse = (Horse)_navigationService.Parameter;
        }

        #region Definitions

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

        private ICommand _backToHorse;

        public ICommand Back
        {
            get
            {
                if (_backToHorse == null)
                {
                    _backToHorse = new RelayCommand(() =>
                    {
                        _navigationService.NavigateTo("ShowHorsePage", MainHorse);
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
