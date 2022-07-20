using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_seminarski.Modelview
{
    public class StrucnaSpremaPrikazWM
    {
        public class Row
        {
            public int StrucnaSpremaID { get; set; }
            public string Naziv { get; set; }

        }

        public List<Row> StrucnaSpremaRow { get; set; }

    }
}
