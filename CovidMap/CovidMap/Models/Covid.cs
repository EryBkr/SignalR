using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CovidMap.Models
{
    public enum ECity
    {
        Istanbul=1,
        Ankara=2,
        Izmir=3,
        Canakkale=4,
        Antalya=5
    }

    public class Covid
    {
        public int Id { get; set; }
        public ECity City { get; set; }
        public int Count { get; set; }
        public DateTime CovidDate { get; set; }
    }
}
