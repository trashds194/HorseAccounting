﻿using GalaSoft.MvvmLight;
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

        public async void OnPageLoad()
        {
            await Task.Run(() =>
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

        public ObservableCollection<Horse> MainHorseList
        {
            get
            {
                return _mainHorseList;
            }

            set
            {
                _mainHorseList = value;
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

        #region MenuCommands

        private RelayCommand _openTaleMagazine;

        public RelayCommand OpenTaleMagazine
        {
            get
            {
                return _openTaleMagazine
                    ?? (_openTaleMagazine = new RelayCommand(
                    () =>
                    {
                        _navigationService.NavigateTo("TaleMagazinePage", MainHorse);
                    }));
            }

            private set
            {
                _openTaleMagazine = value;
            }
        }

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
                        MainHorseList = null;
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
