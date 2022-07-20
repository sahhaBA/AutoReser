using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_seminarski.Modelview
{
    public class GradPrikazWM
    {
        public class Row
        {
            public int GradID { get; set; }
            public string Naziv { get; set; }

        }

        public List<Row> GradoviRow { get; set; }

    }
}
