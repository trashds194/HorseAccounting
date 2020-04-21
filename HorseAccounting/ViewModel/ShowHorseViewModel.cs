using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using HorseAccounting.Infra;
using HorseAccounting.Model;
using System;
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
        private ObservableCollection<TribalUse> _mainTribalUses;
        private ObservableCollection<Progression> _mainHorseProgression;

        private bool _addProgressionVisible;
        private bool _parentsVis;
        private bool _maleVis;
        private bool _stallionVis;

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

                SelectedHorse = Horse.GetSelectedHorseAsync(MainHorse.ID).Result;

                if (SelectedHorse.Gender.Equals("Кобыла"))
                {
                    MaleVis = true;
                    StallionVis = false;
                }
                else
                {
                    MaleVis = false;
                    StallionVis = true;
                }

                try
                {
                    HorseNick = SelectedHorse.FullName;

                    if (SelectedHorse.MotherID != 0 || SelectedHorse.FatherID != 0)
                    {
                        ParentsVis = true;
                        MotherHorse = Horse.GetSelectedHorseAsync(SelectedHorse.MotherID).Result;
                        FatherHorse = Horse.GetSelectedHorseAsync(SelectedHorse.FatherID).Result;
                    }
                    else
                    {
                        ParentsVis = false;
                    }

                    _mainHorseProgression = Progression.GetSelectedProgression(SelectedHorse.ID).Result;
                    _mainTribalUses = TribalUse.GetSelectedTribalUse(SelectedHorse.ID).Result;
                    _mainHorseScoring = Scoring.GetSelectedScoring(SelectedHorse.ID).Result;

                    RaisePropertyChanged(() => MainHorseProgression);
                    RaisePropertyChanged(() => MainHorseScoring);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }).ConfigureAwait(true);
        }

        #region Definitions

        public bool MaleVis
        {
            get
            {
                return _maleVis;
            }

            set
            {
                _maleVis = value;
                RaisePropertyChanged(nameof(MaleVis));
            }
        }

        public bool StallionVis
        {
            get
            {
                return _stallionVis;
            }

            set
            {
                _stallionVis = value;
                RaisePropertyChanged(nameof(StallionVis));
            }
        }

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

        public string AddProgressionButtonText
        {
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

        public ObservableCollection<TribalUse> MainTribalUses
        {
            get
            {
                return _mainTribalUses;
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
                if (_addProgression == null)
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
                                if (Progression.AddProgressionAsync(AddedProgression.Date, AddedProgression.Destination, AddedProgression.Comment, MainHorse.ID).Result)
                                {
                                    Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Вы успешно добавили движение лошади"));
                                    if (AddedProgression.Destination.Equals("продажа") || AddedProgression.Destination.Equals("списание"))
                                    {
                                        if (SelectedHorse.Gender.Equals("Жеребец"))
                                        {
                                            Horse.ChangeHorseStateAsync(SelectedHorse.ID, "Выбыл");
                                        }
                                        else
                                        {
                                            Horse.ChangeHorseStateAsync(SelectedHorse.ID, "Выбыла");
                                        }
                                    }
                                    else
                                    {
                                        if (SelectedHorse.Gender.Equals("Жеребец"))
                                        {
                                            Horse.ChangeHorseStateAsync(SelectedHorse.ID, "Действующий");
                                        }
                                        else
                                        {
                                            Horse.ChangeHorseStateAsync(SelectedHorse.ID, "Действующая");
                                        }
                                    }
                                    AddedProgression.CleanProgressionData();
                                    AddProgressionVisible = false;
                                    AddProgressionButtonText = "Добавить";
                                    _mainHorseProgression = Progression.GetSelectedProgression(SelectedHorse.ID).Result;
                                    RaisePropertyChanged(nameof(MainHorseProgression));
                                    SelectedHorse = Horse.GetSelectedHorseAsync(MainHorse.ID).Result;
                                    RaisePropertyChanged(nameof(SelectedHorse));
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

        private ICommand _addTribalUse;

        public ICommand AddTribalUse
        {
            get
            {
                if (_addTribalUse == null)
                {
                    _addTribalUse = new RelayCommand(() =>
                    {
                        //_navigationService.NavigateTo("", MainHorse);
                    });
                }

                return _addTribalUse;
            }

            set
            {
                _addTribalUse = value;
            }
        }

        #endregion
    }
}
