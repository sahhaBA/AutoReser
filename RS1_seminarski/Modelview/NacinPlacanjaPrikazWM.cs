using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_seminarski.Modelview
{
    public class NacinPlacanjaPrikazWM
    {
        public class Row
        {
            public int NacinPlacanjaID { get; set; }
            public string Naziv { get; set; }

        }

        public List<Row> NacinPlacanjaRow { get; set; }

    }
}
