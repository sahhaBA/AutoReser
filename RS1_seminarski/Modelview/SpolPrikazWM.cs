using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_seminarski.Modelview
{
    public class SpolPrikazWM
    {
        public class Row
        {
            public int SpolID { get; set; }
            public string Naziv { get; set; }

        }

        public List<Row> SpolRow { get; set; }

    }
}
