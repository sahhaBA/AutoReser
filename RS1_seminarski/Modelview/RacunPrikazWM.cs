using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_seminarski.Modelview
{
    public class RacunPrikazWM
    {
        public class Row
        {
            public int RacunID { get; set; }
            public DateTime DatumRacuna { get; set; }
            public int BrojRezervacije { get; set; }  
            public string ImeKlijenta { get; set; }
            public string Napomena { get; set; }
        }

        public List<Row> RacunRows  { get; set; }

    }
}
