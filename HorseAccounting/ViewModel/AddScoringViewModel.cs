using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using HorseAccounting.Infra;
using HorseAccounting.Model;
using System;
using System.Windows.Input;

namespace HorseAccounting.ViewModel
{
    public class AddScoringViewModel : ViewModelBase
    {
        #region Vars

        private IPageNavigationService _navigationService = new PageNavigationService();

        private Horse _mainHorse;
        private Horse _selectedHorse;
        private Scoring _addedScoring;

        private int _birthHorseYear;
        private int _addedScoringYear;

        #endregion

        public AddScoringViewModel(IPageNavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public void OnPageLoad()
        {
            MainHorse = (Horse)_navigationService.Parameter;
            SelectedHorse = Horse.GetSelectedHorseAsync(MainHorse.ID).Result;

            BirthHorseYear = Convert.ToDateTime(SelectedHorse.BirthDate).Year;
        }

        public void CheckForNull()
        {
            if (AddedScoring.Boniter == null)
            {
                AddedScoring.Boniter = string.Empty;
            }
            if (AddedScoring.Comment == null)
            {
                AddedScoring.Comment = string.Empty;
            }
            if (AddedScoring.TheClass == null)
            {
                AddedScoring.TheClass = string.Empty;
            }
        }

        #region Definitions

        public int BirthHorseYear
        {
            get
            {
                return _birthHorseYear;
            }

            set
            {
                _birthHorseYear = value;
                RaisePropertyChanged(nameof(BirthHorseYear));
            }
        }

        public int AddedScoringYear
        {
            get
            {
                return _addedScoringYear;
            }

            set
            {
                _addedScoringYear = value;
                RaisePropertyChanged(nameof(AddedScoringYear));
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
                _selectedHorse = value;
                RaisePropertyChanged(nameof(SelectedHorse));
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
                        _navigationService.NavigateTo("ShowHorsePage", SelectedHorse);
                        AddedScoring.CleanScoringData();
                    });
                }

                return _backToHorse;
            }

            set
            {
                _backToHorse = value;
            }
        }

        private ICommand _setAge;

        public ICommand SetAge
        {
            get
            {
                if (_setAge == null)
                {
                    _setAge = new RelayCommand(() =>
                    {
                        if (!string.IsNullOrEmpty(AddedScoring.Date))
                        {
                            AddedScoringYear = Convert.ToDateTime(AddedScoring.Date).Year;
                            int n = AddedScoringYear - BirthHorseYear;
                            int last;
                            if (n > 19 || n < 10)
                            {
                                last = n % 10;
                                if (last == 1) 
                                { 
                                    AddedScoring.Age = n + " год"; 
                                }
                                else if (last == 0 || last >= 5) 
                                { 
                                    AddedScoring.Age = n + " лет"; 
                                }
                                else 
                                { 
                                    AddedScoring.Age = n + " года"; 
                                }
                            }
                            else AddedScoring.Age = n + " лет";
                        }
                    });
                }

                return _setAge;
            }

            set
            {
                _setAge = value;
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
                        CheckForNull();
                        if (Scoring.AddScoringAsync(AddedScoring.Date, AddedScoring.Age, AddedScoring.Boniter, AddedScoring.Origin, AddedScoring.Typicality, AddedScoring.Measurements, AddedScoring.Exterior, AddedScoring.WorkingCapacity, AddedScoring.OffspringQuality, AddedScoring.TheClass, AddedScoring.Comment, SelectedHorse.ID).Result)
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
