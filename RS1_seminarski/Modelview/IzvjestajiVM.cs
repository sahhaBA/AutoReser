using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_seminarski.Modelview
{
    public class IzvjestajiVM
    {
        public int total { get; set; }
        public List<Izvjestaj> izvjestaji { get; set; }
        public string q { get; set; }
        public class Izvjestaj
        {
            public int izvjestajID { get; set; }
            public int ukupnoZapisa { get; set; }
            public string naziv { get; set; }
            public string opis { get; set; }
            public string automobilID { get; set; }
            public string uposlenikKreiraIzvjestaj { get; set; }

            public string datumIzvjestaja { get; set; }

            public string korisnickiNalogID { get; set; }
        }
    }
}
