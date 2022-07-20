using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_seminarski.Modelview
{
    public class RadnoMjestoPrikazWM
    {
        public class Row
        {
            public int RadnoMjestoID { get; set; }
            public string Naziv { get; set; }

        }

        public List<Row> RadnoMjestoRow { get; set; }

    }
}
