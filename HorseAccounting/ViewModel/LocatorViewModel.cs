using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using System.Windows;

namespace HorseAccounting.ViewModel
{
    public class LocatorViewModel
    {
        public LocatorViewModel()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    // Create run time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DataService>();
            ////}

            SimpleIoc.Default.Register<MainWindowViewModel>();
            SimpleIoc.Default.Register<HorsesListViewModel>();
            SimpleIoc.Default.Register<AddHorseViewModel>();
            Messenger.Default.Register<NotificationMessage>(this, NotifyUserMethod);
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


        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }

        private void NotifyUserMethod(NotificationMessage message)
        {
            MessageBox.Show(message.Notification);
        }
    }
}
