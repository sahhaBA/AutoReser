using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_seminarski.Modelview
{
    public class KategorijaWM
    {
        public class Row
        {
            public int KategorijaID { get; set; }
            public string naziv { get; set; }

        }

        public List<Row> KategorijaRow { get; set; }

    }
}
