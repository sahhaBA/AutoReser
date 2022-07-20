using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_seminarski.Modelview
{
    public class RezervacijaPrikazWM
    {
        public class Row
        {
            public int RezervacijaID { get; set; }
            public DateTime DatumRezervacije { get; set; }
            public bool Aktivna { get; set; }
            public bool Odobrena { get; set; }
            public bool Zavrsena { get; set; }
            public string ImeKlijenta { get; set; }
        }

        public List<Row> RezervacijaRows  { get; set; }

    }
}
