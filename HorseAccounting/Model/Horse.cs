using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HorseAccounting.Model
{
    public class Horse : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public int GpkNum { get; set; }
        public string NickName { get; set; }
        public int Brand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
