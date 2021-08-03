using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CovidMap.Models
{
    //Pivotlanmış data için modelim
    public class PivottedList
    {
        public string Date { get; set; }
        public int TotalIstanbulVariant { get; set; }
        public int TotalAnkaraVariant { get; set; }
        public int TotalIzmirVariant { get; set; }
        public int TotalCanakkaleVariant { get; set; }
        public int TotalAntalyaVariant { get; set; }
    }
}
