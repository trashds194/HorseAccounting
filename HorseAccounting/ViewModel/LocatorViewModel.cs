using GalaSoft.MvvmLight.Ioc;
using CommonServiceLocator;

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
        }

        public MainWindowViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainWindowViewModel>();
            }
        }

        public HorsesListViewModel Page1
        {
            get
            {
                return ServiceLocator.Current.GetInstance<HorsesListViewModel>();
            }
        }

        public AddHorseViewModel Page2
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
    }
}
