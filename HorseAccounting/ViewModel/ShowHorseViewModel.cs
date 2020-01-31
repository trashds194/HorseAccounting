using GalaSoft.MvvmLight.Command;
using HorseAccounting.Infra;
using HorseAccounting.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HorseAccounting.ViewModel
{
    public class ShowHorseViewModel : NavigateViewModel
    {
        #region Vars

        private Horse selectedHorse;
        private ObservableCollection<Horse> selectedHorseList;

        #endregion

        public ShowHorseViewModel()
        {
            Title = "Просмотр лошади";
            selectedHorseList = Horse.GetSelectedHorse();
            this.RaisePropertyChanged(() => this.SelectedHorseList);
        }

        #region Definitions

        public ObservableCollection<Horse> SelectedHorseList
        {
            get
            {
                return selectedHorseList;
            }
        }

        public Horse SelectedHorse
        {
            get
            {
                return selectedHorse;
            }
            set
            {
                selectedHorse = value;
                RaisePropertyChanged(nameof(SelectedHorse));
            }
        }

        #endregion

        #region Commands

        private ICommand _horsesList;
        public ICommand BackToList
        {
            get
            {
                if (_horsesList == null)
                {
                    _horsesList = new RelayCommand(() =>
                    {
                        Navigate("View/HorsesList.xaml");
                    });
                }
                return _horsesList;
            }
            set { _horsesList = value; }
        }

        #endregion
    }
}
