using Podaci.Entiteti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_seminarski.Modelview
{
    public class EvidencijaOpremeVM
    {
        public string PaketOpreme { get; set; }
        public List<Oprema> Oprema { get; set; }
    }
}
