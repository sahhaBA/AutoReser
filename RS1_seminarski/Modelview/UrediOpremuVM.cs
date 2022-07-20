using Podaci.Entiteti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_seminarski.Modelview
{
    public class UrediOpremuVM
    {
        public int PaketOpremeID { get; set; }
        public string PaketOpreme { get; set; }
        public List<Oprema> ListaOpreme { get; set; }
        public int AutomobilID { get; set; }
        public string Automobil { get; set; }
    }
}
