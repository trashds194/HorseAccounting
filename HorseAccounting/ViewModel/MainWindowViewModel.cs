using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using HorseAccounting.Infra;

namespace HorseAccounting.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {

        private IPageNavigationService _navigationService;
        private RelayCommand _loadedCommand;
        public RelayCommand LoadedCommand
        {
            get
            {
                return _loadedCommand
                    ?? (_loadedCommand = new RelayCommand(
                    () =>
                    {
                        _navigationService.NavigateTo("Home");
                    }));
            }
        }

        public MainWindowViewModel(IPageNavigationService navigationService)
        {
            _navigationService = navigationService;
        }
    }
}
