using GalaSoft.MvvmLight;
using MySql.Data.MySqlClient;

namespace HorseAccounting.Model
{
    public class Scoring : ObservableObject
    {
        #region Vars

        private int _id;
        private string _date;
        private int _origin;
        private int _typicality;
        private int _measurements;
        private int _exterior;
        private int _workingCapacity;
        private int _offspringQuality;
        private int _theClass;
        private int _horseID;

        #endregion

        #region Definitions

        public int ID
        {
            get { return _id; } set { Set<int>(() => ID, ref _id, value); }
        }

        public string Date
        {
            get { return _date; } set { Set<string>(() => Date, ref _date, value); }
        }

        public int Origin
        {
            get { return _origin; } set { Set<int>(() => Origin, ref _origin, value); }
        }

        public int Typicality
        {
            get { return _typicality; } set { Set<int>(() => Typicality, ref _typicality, value); }
        }

        public int Measurements
        {
            get { return _measurements; } set { Set<int>(() => Measurements, ref _measurements, value); }
        }

        public int Exterior
        {
            get { return _exterior; } set { Set<int>(() => Exterior, ref _exterior, value); }
        }

        public int WorkingCapacity
        {
            get { return _workingCapacity; } set { Set<int>(() => WorkingCapacity, ref _workingCapacity, value); }
        }

        public int OffspringQuality
        {
            get { return _offspringQuality; } set { Set<int>(() => OffspringQuality, ref _offspringQuality, value); }
        }

        public int TheClass
        {
            get { return _theClass; } set { Set<int>(() => TheClass, ref _theClass, value); }
        }

        public int HorseID
        {
            get { return _horseID; } set { Set<int>(() => HorseID, ref _horseID, value); }
        }

        #endregion

        #region ShowHorsePage

        #endregion
    }
}
