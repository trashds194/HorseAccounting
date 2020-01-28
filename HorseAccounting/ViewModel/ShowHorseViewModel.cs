﻿using GalaSoft.MvvmLight.Command;
using HorseAccounting.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HorseAccounting.ViewModel
{
    public class ShowHorseViewModel : NavigateViewModel
    {
        public ShowHorseViewModel()
        {
            Title = "Просмотр лошади";
        }

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
    }
}
