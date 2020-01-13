﻿using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;

namespace HorseAccounting.Infra
{
    public class NavigateViewModel : ViewModelBase
    {
        public NavigateViewModel()
        {

        }

        public string Title { get; set; }
        public void Navigate(string url)
        {
            Messenger.Default.Send<NavigateArgs>(new NavigateArgs(url));
        }
    }
}
