using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using HorseAccounting.Infra;
using System;
using System.Windows;

namespace HorseAccounting.ViewModel
{
    public class LocatorViewModel
    {
        public LocatorViewModel()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<MainWindowViewModel>();
            SimpleIoc.Default.Register<HorsesListViewModel>();
            SimpleIoc.Default.Register<AddHorseViewModel>();
            SimpleIoc.Default.Register<ChangeHorseViewModel>();
            SimpleIoc.Default.Register<ShowHorseViewModel>();
            SimpleIoc.Default.Register<AddScoringViewModel>();
            SimpleIoc.Default.Register<TaleMagazineViewModel>();
            SimpleIoc.Default.Register<AddTribalUseViewModel>();
            SimpleIoc.Default.Register<AddHorseSimpleViewModel>();
            SimpleIoc.Default.Register<ShowDiagramsViewModel>();
            Messenger.Default.Register<NotificationMessage>(this, NotifyUserMethod);

            SetupNavigation();
        }

        private static void SetupNavigation()
        {
            var navigationService = new PageNavigationService();
            navigationService.Configure("HorsesList", new Uri("../View/HorsesList.xaml", UriKind.Relative));
            navigationService.Configure("AddHorsePage", new Uri("../View/AddHorse.xaml", UriKind.Relative));
            navigationService.Configure("ChangeHorsePage", new Uri("../View/ChangeHorse.xaml", UriKind.Relative));
            navigationService.Configure("ShowHorsePage", new Uri("../View/ShowHorse.xaml", UriKind.Relative));
            navigationService.Configure("AddScoringPage", new Uri("../View/AddScoring.xaml", UriKind.Relative));
            navigationService.Configure("TaleMagazinePage", new Uri("../View/TaleMagazine.xaml", UriKind.Relative));
            navigationService.Configure("AddTribalUsePage", new Uri("../View/AddTribalUse.xaml", UriKind.Relative));
            navigationService.Configure("AddHorseSimpleWindow", new Uri("../View/AddHorseSimple.xaml", UriKind.Relative));
            navigationService.Configure("ShowDiagramsPage", new Uri("../View/ShowDiagrams.xaml", UriKind.Relative));

            SimpleIoc.Default.Register<IPageNavigationService>(() => navigationService);
        }

        public MainWindowViewModel MainWindowViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainWindowViewModel>();
            }
        }

        public HorsesListViewModel HorsesListViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<HorsesListViewModel>();
            }
        }

        public AddHorseViewModel AddHorseViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AddHorseViewModel>();
            }
        }

        public ChangeHorseViewModel ChangeHorseViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ChangeHorseViewModel>();
            }
        }

        public ShowHorseViewModel ShowHorseViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ShowHorseViewModel>();
            }
        }

        public AddScoringViewModel AddScoringViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AddScoringViewModel>();
            }
        }

        public TaleMagazineViewModel TaleMagazineViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<TaleMagazineViewModel>();
            }
        }

        public AddTribalUseViewModel AddTribalUseViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AddTribalUseViewModel>();
            }
        }

        public AddHorseSimpleViewModel AddHorseSimpleViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AddHorseSimpleViewModel>();
            }
        }

        public ShowDiagramsViewModel ShowDiagramsViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ShowDiagramsViewModel>();
            }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels.
        }

        private void NotifyUserMethod(NotificationMessage message)
        {
            MessageBox.Show(message.Notification);
        }
    }
}
