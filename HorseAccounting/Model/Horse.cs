using HorseAccounting.Infra;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HorseAccounting.Model
{
    public class Horse : NotificationClass
    {
        public int Id { get; set; }
        public int GpkNum { get; set; }
        public string NickName { get; set; }
        public int Brand { get; set; }
        public string Bloodiness { get; set; }     
        public string Color { get; set; }
        public string BirthDate { get; set; }
        public string BirthPlace { get; set; }
        public string Owner { get; set; }
    }
}
