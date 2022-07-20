using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_seminarski.Modelview
{
    public class PaketOpremePrikazWM
    {
        public class Row
        {
            public int PaketOpremeID { get; set; }
            public string Naziv { get; set; }

        }

        public List<Row> PaketOpremeRow { get; set; }
    }
}
