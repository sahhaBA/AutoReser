using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_seminarski.Modelview
{
    public class PoreznaStopaWM
    {
        public class Row
        {
            public int PoreznaStopaID { get; set; }
            public string Naziv { get; set; }
            public float Iznos { get; set; }

        }

        public List<Row> PoreznaStopaRows { get; set; }

    }
}
