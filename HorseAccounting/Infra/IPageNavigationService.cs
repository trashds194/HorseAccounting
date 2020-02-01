using GalaSoft.MvvmLight.Views;

namespace HorseAccounting.Infra
{
    public interface IPageNavigationService : INavigationService
    {
        object Parameter { get; }
    }
}
