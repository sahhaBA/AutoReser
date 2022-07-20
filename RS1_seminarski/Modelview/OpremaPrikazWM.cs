using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_seminarski.Modelview
{
    public class OpremaPrikazWM
    {
        public class Row {

        public int OpremaID { get; set; }
        public string OpisOpreme { get; set; }
        public string PaketOpremeNaziv { get; set; }
        }

        public List<Row> OpremaRow { get; set; }

    }
    
}
