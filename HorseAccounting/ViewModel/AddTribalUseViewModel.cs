using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using HorseAccounting.Infra;
using HorseAccounting.Model;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HorseAccounting.ViewModel
{
    public class AddTribalUseViewModel : ViewModelBase
    {
        #region Vars

        private IPageNavigationService _navigationService = new PageNavigationService();

        private Horse _mainHorse;
        private Horse _fatherHorse;

        private TribalUse _addedTribalUse;

        private ObservableCollection<Horse> _fatherHorseList;

        #endregion

        public AddTribalUseViewModel(IPageNavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public async void OnPageLoad()
        {
            await Task.Run(() =>
            {
                MainHorse = (Horse)_navigationService.Parameter;
                ComboBoxesUpdate();
            }).ConfigureAwait(true);
        }

        private async void ComboBoxesUpdate()
        {
            await Task.Run(() =>
            {
                _fatherHorseList = Horse.GetFatherHorseAsync().Result;
                RaisePropertyChanged(() => FatherHorseList);
            }).ConfigureAwait(true);
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

        public Horse FatherHorse
        {
            get
            {
                return _fatherHorse;
            }

            set
            {
                _fatherHorse = value;
                RaisePropertyChanged(nameof(FatherHorse));
            }
        }

        public TribalUse AddedTribalUse
        {
            get
            {
                return _addedTribalUse;
            }

            set
            {
                _addedTribalUse = value;
                RaisePropertyChanged(nameof(AddedTribalUse));
            }
        }

        public ObservableCollection<Horse> FatherHorseList
        {
            get
            {
                return _fatherHorseList;
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

        private ICommand _addTribalUseToList;

        public ICommand AddTribalUseToList
        {
            get
            {
                if(_addTribalUseToList == null)
                {
                    AddedTribalUse = new TribalUse();
                    _addTribalUseToList = new RelayCommand(() =>
                    {
                        if (TribalUse.AddTribalUseAsync(AddedTribalUse.Year, AddedTribalUse.LastDate, FatherHorse.FullName, AddedTribalUse.FatherBreed, AddedTribalUse.FoalClass,
                            AddedTribalUse.FoalDate, AddedTribalUse.FoalGender, AddedTribalUse.FoalColor, AddedTribalUse.FoalNickName, AddedTribalUse.FoalDestination, FatherHorse.ID, 0, MainHorse.ID).Result)
                        {
                            Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Вы успешно добавили племенную деятельность"));
                        }
                        else
                        {
                            Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Не удалось добавить племенную деятельность"));
                        }
                    });
                }

                return _addTribalUseToList;
            }

            set
            {
                _addTribalUseToList = value;
            }
        }

        #endregion
    }
}
