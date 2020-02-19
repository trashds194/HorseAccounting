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

        private ObservableCollection<Progression> _mainHorseProgression;

        private bool _addProgressionVisible;
        private bool _parentsVis;

        private string _addProgressionButtonText;

        private Progression _addedProgression;

        #endregion

        public ShowHorseViewModel(IPageNavigationService navigationService)
        {
            _navigationService = navigationService;          
        }

        public async void OnPageLoad()
        {
            ParentsVis = false;
            AddProgressionVisible = false;

            AddProgressionButtonText = "Добавить";

            await Task.Run(() =>
            {
                MainHorse = (Horse)_navigationService.Parameter;

                SelectedHorse = Horse.GetSelectedHorse(MainHorse.ID);

                HorseNick = SelectedHorse.FullName;

                if (SelectedHorse.MotherID != 0 || SelectedHorse.FatherID != 0)
                {
                    ParentsVis = true;
                    MotherHorse = Horse.GetSelectedHorse(SelectedHorse.MotherID);
                    FatherHorse = Horse.GetSelectedHorse(SelectedHorse.FatherID);
                }
                else
                {
                    ParentsVis = false;
                }

                _mainHorseProgression = Progression.GetSelectedProgression(SelectedHorse.ID);

                _mainHorseScoring = Scoring.GetSelectedScoring(SelectedHorse.ID);

                RaisePropertyChanged(() => MainHorseProgression);
                RaisePropertyChanged(() => MainHorseScoring);
            }).ConfigureAwait(true);
        }

        #region Definitions

        public bool ParentsVis
        {
            get
            {
                return _parentsVis;
            }

            set
            {
                _parentsVis = value;
                RaisePropertyChanged(nameof(ParentsVis));
            }
        }

        public bool AddProgressionVisible
        {
            get
            {
                return _addProgressionVisible;
            }

            set
            {
                _addProgressionVisible = value;
                RaisePropertyChanged(nameof(AddProgressionVisible));
            }
        }

        public string AddProgressionButtonText {
            get
            {
                return _addProgressionButtonText;
            }

            set
            {
                _addProgressionButtonText = value;
                RaisePropertyChanged(nameof(AddProgressionButtonText));
            }
        }

        public Progression AddedProgression
        {
            get
            {
                return _addedProgression;
            }

            set
            {
                _addedProgression = value;
                RaisePropertyChanged(nameof(AddedProgression));
            }
        }

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

        public ObservableCollection<Progression> MainHorseProgression
        {
            get 
            {
                return _mainHorseProgression;
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

        private ICommand _addProgression;

        public ICommand AddProgression
        {
            get
            {
                if(_addProgression == null)
                {
                    AddedProgression = new Progression();
                    _addProgression = new RelayCommand(() =>
                    {
                        if (AddProgressionButtonText.Equals("Добавить"))
                        {
                            AddProgressionVisible = true;
                            AddProgressionButtonText = "Сохранить";
                        }
                        else
                        {
                            if (AddedProgression.Comment == null)
                            {
                                AddedProgression.Comment = string.Empty;
                            }
                            if (!string.IsNullOrEmpty(AddedProgression.Date))
                            {
                                if (Progression.AddProgression(AddedProgression.Date, AddedProgression.Destination, AddedProgression.Comment, MainHorse.ID))
                                {
                                    Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Вы успешно добавили движение лошади"));
                                    AddedProgression.CleanProgressionData();
                                    AddProgressionVisible = false;
                                    AddProgressionButtonText = "Добавить";
                                    _mainHorseProgression = Progression.GetSelectedProgression(SelectedHorse.ID);
                                    RaisePropertyChanged(nameof(MainHorseProgression));
                                }
                                else
                                {
                                    Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Ошибка при добавлении движения, проверьте корректность введенных данных!"));
                                }
                            }
                        }
                    });
                }

                return _addProgression;
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
