using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_seminarski.Modelview
{
    public class StavkeRezervacijePrikazWM
    {
        public class Row
        {
            public int RezervacijaID { get; set; }
            public string ModelAutomobila  { get; set; }
            public float Cijena { get; set; }

        }

        public List<Row> StavkeRows  { get; set; }

    }
}
