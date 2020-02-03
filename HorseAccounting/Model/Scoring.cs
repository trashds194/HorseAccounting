using GalaSoft.MvvmLight;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseAccounting.Model
{
    class Scoring : ObservableObject
    {
        #region Vars

        private int id;
        private string date;
        private int origin;
        private int typicality;
        private int measurements;
        private int exterior;
        private int workingCapacity;
        private int offspringQuality;
        private int theClass;
        private int horseID;

        #endregion

        #region Definitions

        private static MySqlConnection connection { get; set; }

        public int ID { get { return id; } set { Set<int>(() => this.ID, ref id, value); } }
        public string Date { get { return date; } set { Set<string>(() => this.Date, ref date, value); } }
        public int Origin { get { return origin; } set { Set<int>(() => this.Origin, ref origin, value); } }
        public int Typicality { get { return typicality; } set { Set<int>(() => this.Typicality, ref typicality, value); } }
        public int Measurements { get { return measurements; } set { Set<int>(() => this.Measurements, ref measurements, value); } }
        public int Exterior { get { return exterior; } set { Set<int>(() => this.Exterior, ref exterior, value); } }
        public int WorkingCapacity { get { return workingCapacity; } set { Set<int>(() => this.WorkingCapacity, ref workingCapacity, value); } }
        public int OffspringQuality { get { return offspringQuality; } set { Set<int>(() => this.OffspringQuality, ref offspringQuality, value); } }
        public int TheClass { get { return theClass; } set { Set<int>(() => this.TheClass, ref theClass, value); } }
        public int HorseID { get { return horseID; } set { Set<int>(() => this.HorseID, ref horseID, value); } }

        #endregion

        #region ShowHorsePage



        #endregion
    }
}
