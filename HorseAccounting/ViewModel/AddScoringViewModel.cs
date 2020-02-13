using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using HorseAccounting.Infra;
using HorseAccounting.Model;
using System.Windows.Input;

namespace HorseAccounting.ViewModel
{
    public class AddScoringViewModel : ViewModelBase
    {
        #region Vars

        private IPageNavigationService _navigationService = new PageNavigationService();

        private Horse _mainHorse;
        private Scoring _addedScoring;

        #endregion

        public AddScoringViewModel(IPageNavigationService navigationService)
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

        public Scoring AddedScoring
        {
            get
            {
                return _addedScoring;
            }

            set
            {
                _addedScoring = value;
                RaisePropertyChanged(nameof(AddedScoring));
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

        private ICommand _addScoringToList;

        public ICommand AddScoringToList
        {
            get
            {
                if (_addScoringToList == null)
                {
                    AddedScoring = new Scoring();
                    _addScoringToList = new RelayCommand(() =>
                    {
                        if (Scoring.AddScoring(AddedScoring.Date, AddedScoring.Boniter, AddedScoring.Origin, AddedScoring.Typicality, AddedScoring.Measurements, AddedScoring.Exterior, AddedScoring.WorkingCapacity, AddedScoring.OffspringQuality, AddedScoring.TheClass, AddedScoring.Comment, MainHorse.ID))
                        {
                            Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Вы успешно добавили бонитировки"));
                            AddedScoring.CleanScoringData();
                        }
                        else
                        {
                            Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Не удалось добавить бонитировки"));
                        }
                    });
                }

                return _addScoringToList;
            }

            set
            {
                _addScoringToList = value;
            }
        }

        #endregion
    }
}
